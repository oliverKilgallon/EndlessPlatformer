using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [System.Serializable]
    public enum PICKUP_TYPE
    {
        slow,
        fast,
        platform
    }

    public float rotSpeed;
    public float pickupDuration = 3f;
    public float scaleIncrement = 0.2f;
    public GameObject[] pickupEffects;
    public Material[] pickupMaterials;

    private PICKUP_TYPE pickupType;
    private Renderer meshRenderer;
    private new SphereCollider collider;
    private Light pickupGlow;
    private GameObject pickupEffect;
    private PlayerStats playerStats;
    private AudioSource source;
    private Material[] materials;
    public delegate void powerupFinished();
    public static event powerupFinished platformFinished;

    private void Awake()
    {
        //Randomise pickup type on spawn
        switch (Random.Range(0, 3))
        {
            case 0:
                pickupType = PICKUP_TYPE.fast;
                break;
            case 1:
                pickupType = PICKUP_TYPE.slow;
                break;
            case 2:
                pickupType = PICKUP_TYPE.platform;
                break;
        }
    }
    //Acquire mesh renderer and collider for pickup
    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();

        materials = meshRenderer.materials;

        collider = GetComponent<SphereCollider>();

        pickupGlow = GetComponent<Light>();

        source = GetComponent<AudioSource>();

        playerStats = GameManager.instance.player.GetComponent<PlayerStats>();

        #region Determine pickup type and effects
        //Setup visual components based on randomised type
        switch (pickupType)
        {
            
            case PICKUP_TYPE.fast:
                pickupGlow.color = Color.cyan;
                materials[1] = pickupMaterials[0];
                pickupEffect = pickupEffects[0];
                break;
            case PICKUP_TYPE.slow:
                pickupGlow.color = Color.red;
                materials[1] = pickupMaterials[1];
                pickupEffect = pickupEffects[1];
                break;
            case PICKUP_TYPE.platform:
                pickupGlow.color = Color.green;
                materials[1] = pickupMaterials[2];
                pickupEffect = pickupEffects[2];
                break;
            default:
                pickupGlow.color = Color.cyan;
                materials[1] = pickupMaterials[0];
                pickupEffect = pickupEffects[0];
                break;
        }

        meshRenderer.materials = materials;

        #endregion
    }

    //Only triggers on player collision
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Spawn relevant pickup effect
            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            StartCoroutine( Collect(other) );
        }
    }

    //Determines pickup behaviour from pickup type enum
    private IEnumerator Collect(Collider player)
    {
        source.Play();

        //Disable visible pickup components + collider to appear "collected"
        meshRenderer.enabled = false;

        collider.enabled = false;

        pickupGlow.enabled = false;

        Vector3 originalScale = player.transform.localScale;
        
        switch (pickupType)
        {
            case PICKUP_TYPE.slow:

                #region Slow powerup

                //E.G if scaleIncrement is 0.2 then player will scale to 6/5ths original size
                while ( player.transform.localScale.magnitude <= playerStats.defaultScale.magnitude * ( 1f + scaleIncrement ) )
                {
                    player.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                    yield return null;
                }
                
                //Decrease player speed and jump multiplier
                playerStats.jumpMultiplier = playerStats.GetDefaultJumpMultiplier() - 1.5f;

                playerStats.speed = playerStats.GetDefaultSpeed() - 3f;

                yield return new WaitForSeconds(pickupDuration);

                //Return player to original size
                while (player.transform.localScale.magnitude > playerStats.defaultScale.magnitude)
                {
                    player.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

                    yield return null;
                }

                //Reset player stats
                playerStats.jumpMultiplier = playerStats.GetDefaultJumpMultiplier();

                playerStats.speed = playerStats.GetDefaultSpeed();
                
                break;

            #endregion

            case PICKUP_TYPE.fast:

                #region Fast powerup

                //E.G if scaleIncrement is 0.2 then player will scale to 4/5ths original size
                while (player.transform.localScale.x > playerStats.defaultScale.magnitude * ( 1f - 2 * scaleIncrement ) )
                {
                    player.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

                    yield return null;
                }
                
                //Increase player speed and jump multiplier
                playerStats.jumpMultiplier = playerStats.GetDefaultJumpMultiplier() + 2f;

                playerStats.speed = playerStats.GetDefaultSpeed() + 3f;

                yield return new WaitForSeconds(pickupDuration);


                //Return player to original scale
                while (player.transform.localScale.magnitude < playerStats.defaultScale.magnitude)
                {
                    player.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

                    yield return null;
                }
                
                //Reset player speed and jump stats
                playerStats.jumpMultiplier = playerStats.GetDefaultJumpMultiplier();

                playerStats.speed = playerStats.GetDefaultSpeed();

                break;

            #endregion

            case PICKUP_TYPE.platform:

                #region Platform powerup
                playerStats.isPlatformPowerupActive = true;

                yield return new WaitForSeconds(pickupDuration);

                playerStats.isPlatformPowerupActive = false;

                if (platformFinished != null)
                {
                    platformFinished();
                }
                break;
                #endregion

            default:
                break;
        }

        Destroy(gameObject);
    }

    //Rotate for a nice visual effect
    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 1, 0) * rotSpeed * Time.deltaTime;
    }
}
