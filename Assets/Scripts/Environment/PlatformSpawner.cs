using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public float distanceBetween;
    public Transform generationPoint;
    public GameObject platform;
    public GameObject powerup;

    //Limits of spawn area
    public GameObject leftLimit;
    public GameObject rightLimit;
    
    private float platformHeight;

    private void Start()
    {
        platformHeight = platform.GetComponent<BoxCollider>().size.y;
    }

    void Update ()
    {
        SpawnPlatforms();
	}

    private void SpawnPlatforms()
    {
        if (transform.position.y - generationPoint.position.y < 4f)
        {
            transform.position = new Vector3(Random.Range(-4, 4), transform.position.y + platformHeight + distanceBetween, 0);

            GameObject newPlatform = Instantiate(platform,transform.position, Quaternion.identity);

            GameManager.instance.platforms.Add(newPlatform);

            if (Random.value < 0.5f)
            {
                Instantiate(powerup, (transform.position + new Vector3(0, 1, 0)), powerup.transform.rotation);
            }
        }
    }
}
