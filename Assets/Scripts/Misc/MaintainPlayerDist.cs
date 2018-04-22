using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainPlayerDist : MonoBehaviour {

    public float divisor;

    private float offset;
    private float distance;

    private void Start()
    {
        offset = Vector3.Distance(GameManager.instance.player.transform.position, gameObject.transform.position);
    }
    void Update ()
    {
        distance = Vector3.Distance(GameManager.instance.player.transform.position, gameObject.transform.position) - offset;
        if (distance != 0)
        {
            StartCoroutine(MaintainPlayerDistance());
        }
	}

    private IEnumerator MaintainPlayerDistance()
    {
        while (distance != 0)
        {
            transform.position += (new Vector3(0, 1f, 0) / divisor) * Mathf.Sign(distance);
            yield return null;
        }
    }
}
