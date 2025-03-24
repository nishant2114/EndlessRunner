using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 600;
    public float gravityModifier = 2;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public bool isDead = false;
    public UIManager uiManager;

    private int currentLane = 1; // 0 = Left, 1 = Center, 2 = Right
    public float laneDistance = 6f; // Distance between lanes
    public float sideSpeed = 15f; // Speed of side movement

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);

        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        ResetPlayer();
    }

    void ResetPlayer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        isOnGround = true;
        currentLane = 1;
    }

    void Update()
    {
        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            Jump();
        }

        // Left and Right Movement
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane > 0)
        {
            MoveLeft(); // Moveright
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane < 2)
        {
            MoveRight(); // Move left
        }

        // Smoothly Move to the Target Lane
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, (currentLane - 1) * laneDistance);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, sideSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    public void MoveLeft()
    {
        if (currentLane > 0 && !gameOver)
        {
            currentLane--;
        }
    }

    public void MoveRight()
    {
        if (currentLane < 2 && !gameOver)
        {
            currentLane++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!!!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            isDead = true;
            uiManager.ShowRestartButton();
        }
    }
}