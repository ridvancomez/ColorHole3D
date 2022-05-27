using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDistributionManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.GetInt("LevelIndex");
        PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") % SceneManager.sceneCountInBuildSettings);
        if(PlayerPrefs.GetInt("LevelIndex") == 0 )
        {
            PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") +1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelIndex") );
    }

}
