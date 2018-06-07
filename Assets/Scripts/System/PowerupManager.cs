using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

    public float powerupDuration = 5f;
    public float maxPowerupDuration = 5f;

    //Powerup effect values
    public float jumpBuff = 2f;
    public float movementDebuff = -3.5f;
    public bool platformPhaseBuffOn = true;

    private Dictionary< Pickup.PICKUP_TYPE, float > powerups = new Dictionary<Pickup.PICKUP_TYPE, float>();

    public delegate void DelPowerupUpdated(Pickup.PICKUP_TYPE type);

    //Pickup events
    public static event DelPowerupUpdated OnPickupExpired;
    public static event DelPowerupUpdated OnPickupCollected;

    //Manager access
    public static PowerupManager instance;

    //Should this be a singleton manager?
    private PlayerStats playerStats;

    //Singleton setup, Delegate and powerup setup
    //as well as acquiring the player stats script
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        OnPickupCollected += ApplyEffect;
        OnPickupCollected += AddPowerupTime;

        OnPickupExpired += RemoveEffect;

        RegisterPowerup(Pickup.PICKUP_TYPE.fast);
        RegisterPowerup(Pickup.PICKUP_TYPE.slow);
        RegisterPowerup(Pickup.PICKUP_TYPE.platform);

        playerStats = GameManager.instance.player.GetComponent<PlayerStats>();

        DontDestroyOnLoad(gameObject);
    }

    //Unsubscribing from delegates
    private void OnDisable()
    {
        OnPickupCollected -= ApplyEffect;
        OnPickupCollected -= AddPowerupTime;

        OnPickupExpired -= RemoveEffect;
    }

    //Return duration of the passed in powerup
    public float GetPowerupTime(Pickup.PICKUP_TYPE type)
    {
        return powerups[type];
    }

    //Add powerup with 0 duration to dictionary
    private void RegisterPowerup(Pickup.PICKUP_TYPE type)
    {
        powerups.Add(type, 0f);
    }
    
    //Decrements the duration of each powerup if it is greater than 0
    //then calls the OnPickupExpired delegate if the duration has hit 0
    private void Update()
    {
        foreach (Pickup.PICKUP_TYPE type in powerups.Keys)
        {
            float duration = powerups[type];

            if (duration > 0f)
            {
                powerups[type] -= Time.deltaTime * 1f;

                if (duration <= 0)
                {
                    if (OnPickupExpired != null) OnPickupExpired(type);

                    duration = 0;
                }
            }
        }
    }

    //Add time to the passed in powerup's duration
    public void AddPowerupTime(Pickup.PICKUP_TYPE powerupType)
    {
        powerups[powerupType] += powerupDuration;

        if (powerups[powerupType] > 5f) powerups[powerupType] = 5f;
    }

    //Apply powerup effect to player based on type
    public void ApplyEffect(Pickup.PICKUP_TYPE powerupType)
    {
        if (powerupType.Equals(Pickup.PICKUP_TYPE.fast))
            playerStats.jumpMultiplier = playerStats.GetDefaultJumpMultiplier() + jumpBuff;

        if (powerupType.Equals(Pickup.PICKUP_TYPE.slow))
            playerStats.speed = playerStats.GetDefaultSpeed() + movementDebuff;

        if (powerupType.Equals(Pickup.PICKUP_TYPE.platform))
            playerStats.isPlatformPowerupActive = platformPhaseBuffOn;
    }

    //Remove powerup effect on player based on type
    public void RemoveEffect(Pickup.PICKUP_TYPE powerupType)
    {
        if (powerupType.Equals(Pickup.PICKUP_TYPE.fast))
            playerStats.jumpMultiplier = playerStats.GetDefaultJumpMultiplier();

        if (powerupType.Equals(Pickup.PICKUP_TYPE.slow))
            playerStats.speed = playerStats.GetDefaultSpeed();

        if (powerupType.Equals(Pickup.PICKUP_TYPE.platform))
            playerStats.isPlatformPowerupActive = !platformPhaseBuffOn;
    }
}
