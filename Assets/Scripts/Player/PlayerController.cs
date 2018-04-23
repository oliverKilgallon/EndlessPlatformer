using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int timesJumped;
    private Rigidbody rb;
    private PlayerStats playerStats;
    private AudioSource[] audioSources;
    private bool jumpRequest;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        timesJumped = 0;

        audioSources = GetComponents<AudioSource>();

        playerStats = GetComponent<PlayerStats>();

    }
	
	void Update ()
    {
        LeftRightMove(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump") && !(timesJumped >= playerStats.jumpLimit))
        {
            jumpRequest = true;
            timesJumped++;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector3.up * playerStats.jumpMultiplier, ForceMode.Impulse);

            timesJumped++;

            jumpRequest = false;

            GetAudioSource("Jump").Play();
        }
    }

    private void LeftRightMove(float leftRightAxis)
    {
        Vector3 velocity;

        if ( ( leftRightAxis < 0 && rb.velocity.x > 0 ) || ( leftRightAxis > 0 && rb.velocity.x < 0 ) )
            velocity = new Vector3(leftRightAxis * playerStats.speed * 2.5f, 0, 0);
        else
            velocity = new Vector3(leftRightAxis * playerStats.speed, 0, 0);

        rb.velocity += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            if (GetAudioSource("Landing2") && Time.time > 0.2f)
                GetAudioSource("Landing2").PlayOneShot(GetAudioSource("Landing2").clip);

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
