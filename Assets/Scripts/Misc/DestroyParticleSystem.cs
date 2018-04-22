using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour {

    private void Update()
    {
        Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().main.duration - 0.1f);
    }
}
