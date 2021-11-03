using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private ParticleSystem dirtParticleFX;
    [SerializeField] private ParticleSystem explosionParticleFX;
    [SerializeField] private Transform startPosition;

    private Animator playerAnim;
    private AudioSource playerAudio;
    private Rigidbody playerRb;
    private bool jumpInput = false, isOnGround = true, doubleJump = true;
    private float jumpForce = 600f, doubleJumpForce = 300f, gravityModifier = 1.5f;

    public bool gameOver = true;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        StartCoroutine(nameof(PlayIntro));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;            
        }

        Time.timeScale = Input.GetKey(KeyCode.A) ? 2f : 1f;         //Accelerate game while key "A" is held
    }

    private void FixedUpdate()
    {
        if (jumpInput && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            jumpInput = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticleFX.Stop();
        }

        //Double jump functionality
        if (jumpInput && doubleJump && !isOnGround)   
        {
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            doubleJump = false;
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticleFX.Play();
            isOnGround = true;
            doubleJump = true;
            jumpInput = false;
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            dirtParticleFX.Stop();
            explosionParticleFX.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

    IEnumerator PlayIntro()
    {
        playerAnim.SetFloat("Speed_Multiplier", 0.5f);

        while(transform.position.x < startPosition.position.x)
        {
            transform.position += Vector3.right * 0.05f;
            yield return null;
        }

        playerAnim.SetFloat("Speed_Multiplier", 1f);
        gameOver = false;
    }
}
