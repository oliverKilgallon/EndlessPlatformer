using UnityEngine;

public class Killzone : MonoBehaviour {
    
    public GameObject leftEnd;
    public GameObject rightEnd;
    public float ascendSpeed = 0.5f;

    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

        line.SetPosition(0, leftEnd.transform.position);

        line.SetPosition(1, rightEnd.transform.position);
    }
    private void Update ()
    {
        transform.position += new Vector3(0, 1, 0) * ascendSpeed * Time.deltaTime;
        transform.position = new Vector3(GameManager.instance.player.transform.position.x, transform.position.y, transform.position.z);
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
