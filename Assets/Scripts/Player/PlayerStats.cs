using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum PLAYER_STATE
    {
        ALIVE,
        DEAD
    }

    //Default scale
    [HideInInspector]
    public Vector3 defaultScale = new Vector3(1f, 1f, 1f);

    [HideInInspector]
    public int powerupsStacked;

    [HideInInspector]
    public int score;
    
    [HideInInspector]
    public Vector3 highestHeight;

    public int jumpLimit;
    public float speed;
    public float jumpMultiplier;

    private PLAYER_STATE playerState;

    //Default values
    private int defaultJumpLimit;
    private float defaultSpeed;
    private float defaultJumpMultiplier;
    
    //Init current health to maximum to start as well as initial player state
    private void Start()
    {
        defaultJumpLimit = jumpLimit;
        defaultSpeed = speed;
        defaultJumpMultiplier = jumpMultiplier;

        powerupsStacked = 0;
        score = PlayerPrefs.GetInt("highScore", 0);
        highestHeight = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (transform.position.y > PlayerPrefs.GetInt("highScore", 0))
        {
            highestHeight = transform.position;
            score = (int) highestHeight.y;
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("highScore", 0);
    }

    #region Getters

    public PLAYER_STATE GetPlayerState()
    {
        return playerState;
    }

    public int GetDefaultJumpLimit()
    {
        return defaultJumpLimit;
    }

    public float GetDefaultJumpMultiplier()
    {
        return defaultJumpMultiplier;
    }
    
    public float GetDefaultSpeed()
    {
        return defaultSpeed;
    }

    #endregion

    #region Setters

    public void SetPlayerState(PLAYER_STATE newPlayerState)
    {
        playerState = newPlayerState;
    }
   
    #endregion
}
