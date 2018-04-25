using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject landingEffect;
    private int timesJumped;
    private Rigidbody rb;
    private PlayerStats playerStats;
    private AudioSource[] audioSources;
    private bool jumpRequest;

    void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        timesJumped = 0;

        playerStats = GetComponent<PlayerStats>();
    }
	
	void Update ()
    {
        if (!playerStats.isPaused)
        {
            LeftRightMove(Input.GetAxis("Horizontal"));

            JumpCheck();
        }

        if (Input.GetKeyDown("escape") && !playerStats.GetPlayerState().Equals(PlayerStats.PLAYER_STATE.DEAD))
        {
            GameManager.instance.GetComponent<UI_ShowPanels>().ToggleMenus();
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

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && !(timesJumped >= playerStats.jumpLimit))
        {
            jumpRequest = true;
            timesJumped++;
        }
    }

    private void LeftRightMove(float leftRightAxis)
    {
        Vector3 velocity;

        if ( ( leftRightAxis < 0 && rb.velocity.x > 0 ) || ( leftRightAxis > 0 && rb.velocity.x < 0 ) )
            velocity = new Vector3(leftRightAxis * playerStats.speed * 3f, 0, 0);
        else
            velocity = new Vector3(leftRightAxis * playerStats.speed, 0, 0);

        rb.velocity += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the collision object is a platform
        if (collision.gameObject.tag.Equals("Platform"))
        {
            //Get and play the landing sound if time is greater than 0.3 to stop sound playing at level start
            if (GetAudioSource("Landing2") && Time.time > 0.3f)
                GetAudioSource("Landing2").PlayOneShot(GetAudioSource("Landing2").clip, GetComponent<AudioSource>().volume);

            if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0 && Time.time > 0.2f)
            {
                timesJumped = 0;
                Vector3 rotation = new Vector3(-90, 0, 0);
                Instantiate(landingEffect, transform.position - new Vector3(0, 0.2f, 0), Quaternion.Euler(rotation));
            }
        }
    }

    //Iterate through audio sources attached to the gameObject by name
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
