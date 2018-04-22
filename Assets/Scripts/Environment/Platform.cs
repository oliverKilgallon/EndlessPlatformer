using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private bool alreadyCheckedForFall = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyCheckedForFall)
        {
            if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) < 0)
            {
                StartCoroutine(WillPlatformFall());
            }
            
        }
    }

    private IEnumerator WillPlatformFall()
    {
        float fallChance = Random.Range(0f, 101f);

        if (fallChance < 25f)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;

            yield return new WaitForSeconds(1f);

            if(!GetComponent<Rigidbody>())
                gameObject.AddComponent<Rigidbody>();
        }

        alreadyCheckedForFall = true;
    }

    private void Update()
    {
        if (Vector3.Distance(
            GameManager.instance.player.transform.position, transform.position) > GameManager.instance.platformDeletionDistance && 
            transform.position.y < GameManager.instance.player.transform.position.y)
            DestroySelf();
    }

    void DestroySelf()
    {
        for (int i = 0; i < GameManager.instance.platforms.Count; i++)
        {
            if (GameManager.instance.platforms[i].GetInstanceID() == gameObject.GetInstanceID())
            {
                GameManager.instance.platforms.RemoveAt(i);
                Destroy(gameObject);
            }
        }
    }
}
