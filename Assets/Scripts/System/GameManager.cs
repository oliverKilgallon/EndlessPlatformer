using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public GameObject player;

    public float platformDeletionDistance;

    [HideInInspector]
    public List<GameObject> platforms;

    public static GameManager instance = null;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            enabled = false;
            return;
        }

        //DontDestroyOnLoad(gameObject);
    }

    private void Update ()
    {
        if (player.GetComponent<PlayerStats>().GetPlayerState().Equals(PlayerStats.PLAYER_STATE.DEAD))
        {
            //PlayerPrefs.SetInt("highScore", player.GetComponent<PlayerStats>().score);

            EndGame();
        }
	}

    public void EndGame()
    {
        HighScoreCheck();

        GetComponent<UI_ShowPanels>().gameOverPanel.SetActive(true);
        GetComponent<UI_ShowPanels>().pausePanel.SetActive(false);
        GetComponent<UI_ShowPanels>().optionsPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        if (!Application.isEditor)
            Application.Quit();
#if UNITY_EDITOR
        else
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");

        //Reset player position and velocity
        instance.player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        instance.player.transform.position = new Vector3(0, 1, 0);

        //Unpause game
        
        GetComponent<UI_ShowPanels>().gameOverPanel.SetActive(false);
        GetComponent<UI_ShowPanels>().pausePanel.SetActive(false);
        GetComponent<UI_ShowPanels>().optionsPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void HighScoreCheck()
    {
        if (player.GetComponent<PlayerStats>().score > PlayerPrefs.GetInt("highScore"))
            PlayerPrefs.SetInt("highScore", player.GetComponent<PlayerStats>().score);
    }
}
