using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int timesJumped;
    private Rigidbody rb;
    private AudioSource[] audioSources;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        timesJumped = 0;

        audioSources = GetComponents<AudioSource>();

    }
	
	void Update ()
    {
        LeftRightMove(Input.GetAxis("Horizontal"));
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && (timesJumped != GetComponent<PlayerStats>().jumpLimit))
        {
            rb.AddForce(Vector3.up * GetComponent<PlayerStats>().jumpMultiplier, ForceMode.Impulse);

            timesJumped++;

            GetAudioSource("Jump").Play();
        }
    }

    private void LeftRightMove(float leftRightAxis)
    {

        Vector3 velocity = new Vector3(leftRightAxis * GetComponent<PlayerStats>().speed, 0, 0);

        rb.velocity += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            if (GetAudioSource("Landing2") && Time.time > 0.2f)
                GetAudioSource("Landing2").Play();

            if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0)
            {
                timesJumped = 0;                
            }
        }
    }

    private AudioSource GetAudioSource(string name)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].clip.name == name)
                return audioSources[i];
        }

        return null;
    }
}
