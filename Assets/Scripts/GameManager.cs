using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameStates
{
    Start,
    StartToPlay,
    Level1,
    LevelPass,
    Level2,
    EndOfLevel,
    LevelBurning
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fingerSprite;

    [SerializeField]
    private GameObject level1Gathers;

    [SerializeField]
    private GameObject level2Gathers;

    [SerializeField]
    private GameObject restartPanel;

    Animator doorAnim;

    internal GameStates CurrentState { get; private set; }

    [SerializeField]
    private ParticleSystem finishParticle;

    [SerializeField]
    private TextMeshProUGUI firstLevelText;

    [SerializeField]
    private TextMeshProUGUI secondLevelText;

    private float vibrationTime;
    private bool isVibration;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(0, 15, 30);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(45, 180, 0));
        restartPanel.SetActive(false);
        doorAnim = GameObject.FindGameObjectWithTag("Kapi").GetComponent<Animator>();
        CurrentState = GameStates.Start;

        firstLevelText.text = (SceneManager.GetActiveScene().buildIndex).ToString();
        secondLevelText.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case GameStates.Start:
                StateStart();
                break;
            case GameStates.StartToPlay:
                StateStartToPlay();
                break;
            case GameStates.Level1:
                StateLevel1();
                break;
            case GameStates.LevelPass:
                StateLevelPass();
                break;
            case GameStates.Level2:
                StateLevel2();
                break;
            case GameStates.EndOfLevel:
                StateEndOfLevel();
                break;
            case GameStates.LevelBurning:
                StateLevelBurning();
                break;
        }
    }

    private void StateStart()
    {
        fingerSprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }
    private void StateStartToPlay()
    {
        fingerSprite.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        CurrentState = GameStates.Level1;
    }

    private void StateLevel1()
    {
        if (level1Gathers.transform.childCount == 0)
        {
            CurrentState = GameStates.LevelPass;
        }
    }

    private void StateLevelPass()
    {

        doorAnim.SetBool("IsOpen", true);
    }

    private void StateLevel2()
    {
        if (level2Gathers.transform.childCount == 0)
        {
            CurrentState = GameStates.EndOfLevel;
        }
    }

    private void StateEndOfLevel()
    {
        PlayerPrefs.SetInt("LevelIndex", (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        if(!finishParticle.isPlaying)
        {
            finishParticle.Play();
        }
        StartCoroutine("NewLevel");
    }

    private void StateLevelBurning()
    {
        vibrationTime +=Time.deltaTime;
        if (!isVibration)
        {
            isVibration = true;
            StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.5f, .3f));
        }
        else if(vibrationTime > .5f)
        {
            restartPanel.SetActive(true);

            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator NewLevel()
    {
        yield return new WaitForSeconds(3);

        //Round robin kullandým çünkü son seviyeyi oynadýktan sonra sahne bulunamadý hatasý verecektir.
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex +1) % SceneManager.sceneCountInBuildSettings);
    }


    internal void StateToBeChanged(GameStates differentState)
    {
        CurrentState = differentState;
    }
}
