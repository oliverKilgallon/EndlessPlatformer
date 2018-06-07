using UnityEngine;

public class UI_PowerupIcons : MonoBehaviour {

    public GameObject jumpPowerup;
    public GameObject movePowerup;
    public GameObject platformPowerup;

    private void Awake()
    {
        PowerupManager.OnPickupCollected += ShowPowerupIcon;
        PowerupManager.OnPickupExpired += HidePowerupIcon;
    }

    private void OnDisable()
    {
        PowerupManager.OnPickupCollected -= ShowPowerupIcon;
        PowerupManager.OnPickupExpired -= HidePowerupIcon;
    }

    public void ShowPowerupIcon(Pickup.PICKUP_TYPE type)
    {
        if (type == Pickup.PICKUP_TYPE.fast)
            jumpPowerup.SetActive(true);

        if (type == Pickup.PICKUP_TYPE.slow)
            movePowerup.SetActive(true);

        if (type == Pickup.PICKUP_TYPE.platform)
            platformPowerup.SetActive(true);
    }

    public void HidePowerupIcon(Pickup.PICKUP_TYPE type)
    {
        if (type == Pickup.PICKUP_TYPE.fast)
            jumpPowerup.SetActive(false);

        if (type == Pickup.PICKUP_TYPE.slow)
            movePowerup.SetActive(false);

        if (type == Pickup.PICKUP_TYPE.platform)
            platformPowerup.SetActive(false);
    }
}
