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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    private void Update ()
    {
        if (player.GetComponent<PlayerStats>().GetPlayerState() == PlayerStats.PLAYER_STATE.DEAD)
        {
            PlayerPrefs.SetInt("highScore", player.GetComponent<PlayerStats>().score);

            EndGame();
        }
	}

    public void EndGame()
    {
        if (player.GetComponent<PlayerStats>().score > PlayerPrefs.GetInt("highScore"))
            PlayerPrefs.SetInt("highScore", player.GetComponent<PlayerStats>().score);

        if (!Application.isEditor)
            Application.Quit();
        #if UNITY_EDITOR
        else
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
