using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    public bool trackingLeft;

    private Text scoreText;
    private Vector3 playerPos;
    private int trackingNumber;
    private float xPos;

	void Start ()
    {
        scoreText = GetComponentInChildren<Text>();
        if (trackingLeft)
            trackingNumber = 0;
        else
        {
            trackingNumber = 1;
        }

        xPos = (Screen.width * trackingNumber) + 50f + (-1f * 100f * trackingNumber);
    }
	
	void Update ()
    {
        playerPos = Camera.main.WorldToScreenPoint( GameManager.instance.player.transform.position );

        transform.position = new Vector3( xPos, playerPos.y, 0 );

        if (transform.position.y > Screen.height)
            transform.position = new Vector3( xPos, Screen.height - 50f, 0 );
        else if (transform.position.y < 0f)
            transform.position = new Vector3( xPos, 50f, 0 );

        if (!trackingLeft)
        {
            scoreText.text = "Highest: " + "\n" + GameManager.instance.player.GetComponent<PlayerStats>().score.ToString() + "m";
        }
        else
            scoreText.text = "Current: " + "\n" + GameManager.instance.player.GetComponent<PlayerStats>().currentScore.ToString() + "m";
	}
}
