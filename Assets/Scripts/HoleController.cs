using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    private GameObject camera;
    private bool ortalandi;
    GameManager gameManager;
    [SerializeField]
    private float speed = 5;

    private float horizontal;
    private float vertical;

    private bool runThePlay;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>().gameObject;
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        HoleMove();

        if (GameObject.FindObjectOfType<GameManager>().CurrentState == GameStates.LevelPass)
        {
            ToDoLevelPass();
        }
    }

    IEnumerator Ortala()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, transform.position.z), 0.025f);
        yield return new WaitForSeconds(2f);
        ortalandi = true;
    }

    private void ToDoLevelPass()
    {
        if (!ortalandi)
        {
            StartCoroutine("Ortala");
        }
        else
        {
            if (transform.position.z > -9)
            {
                camera.transform.SetParent(transform);
                transform.Translate(Vector3.back * 7.5f * Time.deltaTime);
            }
            else
            {
                gameManager.StateToBeChanged(GameStates.Level2);
                camera.transform.SetParent(null);

            }
        }
    }

    private void HoleMove()
    {
        float horizontalValue;
        float verticalValue;
        if (Input.GetMouseButton(0))
        {
            if (!runThePlay)
            {
                gameManager.StateToBeChanged(GameStates.StartToPlay);
            }
            runThePlay = true;
            horizontal = Input.GetAxisRaw("Mouse X");
            vertical = Input.GetAxisRaw("Mouse Y");
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }

        horizontalValue = horizontal * speed * Time.deltaTime;
        verticalValue = vertical * speed * Time.deltaTime;

        transform.Translate(-horizontalValue, 0, -verticalValue);

        //X Pozisyon Kontrol
        if (transform.position.x < -3.6f)
        {
            transform.position = new Vector3(-3.6f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 3.6f)
        {
            transform.position = new Vector3(3.6f, transform.position.y, transform.position.z);
        }

        //Z Pozisyon Kontrol

        if (gameManager.CurrentState == GameStates.Level1)
        {
            ZPosControl(6.5f, 23.5f);
        }
        else if (gameManager.CurrentState == GameStates.Level2)
        {
            ZPosControl(-23f, -7f);
        }
    }


    private void ZPosControl(float minZpos, float maxZpos)
    {
        if (transform.position.z < minZpos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZpos);
        }
        else if (transform.position.z > maxZpos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZpos);
        }
    }
}
