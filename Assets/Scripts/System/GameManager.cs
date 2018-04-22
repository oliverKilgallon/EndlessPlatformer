using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

        DontDestroyOnLoad(gameObject);
    }

    private void Update ()
    {
        if (player.GetComponent<PlayerStats>().GetPlayerState() == PlayerStats.PLAYER_STATE.DEAD)
        {
            if (!Application.isEditor)
                Application.Quit();
#if UNITY_EDITOR
            else
                EditorApplication.isPlaying = false;
#endif
        }
	}
}
