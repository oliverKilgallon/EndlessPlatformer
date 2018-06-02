using UnityEngine;

public class Killzone : MonoBehaviour {
    
    public GameObject leftEnd;
    public GameObject rightEnd;
<<<<<<< HEAD
    public float baseSpeed = 0.5f;
    public float divisor = 5f;
    private float ascentSpeed;
=======
    public float baseSpeed;
    public float divisor;
>>>>>>> Adding builds to git repo

    private float ascentSpeed;
    private LineRenderer line;

    private PlayerStats playerStats;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

        line.SetPosition(0, leftEnd.transform.position);

        line.SetPosition(1, rightEnd.transform.position);

        playerStats = GameManager.instance.player.GetComponent<PlayerStats>();
    }

    private void Update ()
    {
<<<<<<< HEAD
        if (!playerStats.isPaused)
        {
            //Ascent speed is dependant on distance from player + a base speed
            transform.position +=
                new Vector3(0, 1, 0) *
                    (baseSpeed +
                        ((GameManager.instance.player.transform.position.y - transform.position.y) / divisor)
                    )
                * Time.deltaTime;
            transform.position = new Vector3(GameManager.instance.player.transform.position.x, transform.position.y, transform.position.z);
        }

        //Reset line positions
=======
        //Speed is greater the further the killzone is from the player
        ascentSpeed = baseSpeed + 
            ( -( transform.position.y - GameManager.instance.player.transform.position.y ) / 5f);
        
        //Adjust position
        transform.position += new Vector3(0, 1, 0) * ascentSpeed * Time.deltaTime;

        transform.position = new Vector3(
            GameManager.instance.player.transform.position.x, 
            transform.position.y, 
            transform.position.z);

        //Reset line renderer positions
>>>>>>> Adding builds to git repo
        line.SetPosition(0, leftEnd.transform.position);
        line.SetPosition(1, rightEnd.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            other.GetComponent<PlayerStats>().SetPlayerState(PlayerStats.PLAYER_STATE.DEAD);
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(other.gameObject);
        }
    }
}
