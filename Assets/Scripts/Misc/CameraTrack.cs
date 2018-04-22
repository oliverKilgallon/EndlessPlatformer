using UnityEngine;

public class CameraTrack : MonoBehaviour {

    private GameObject player;
    public float trackSpeed = 10f;
    public Vector2 offset;
    public bool trackBothAxes;
    private Vector3 movePercent;

    private void Start()
    {
        player = GameManager.instance.player;
    }
    void LateUpdate ()
    {
        Vector2 distToTarget = new Vector2(
            player.transform.position.x + offset.x - transform.position.x,
            player.transform.position.y + offset.y - transform.position.y);
        if(trackBothAxes)
            movePercent = new Vector3(distToTarget.x, distToTarget.y, 0) / 10f;
        else
            movePercent = new Vector3(0, distToTarget.y, 0) / 10f;

        transform.position += trackSpeed * movePercent * Time.deltaTime;
	}
}
