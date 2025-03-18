using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private GameObject secondBackground;
    private PlayerController playerControllerScript; // Reference to PlayerController
    public float speed = 5f; // Background movement speed

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x; // Get full width

        // Get reference to PlayerController
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Manually create a second background by duplicating the current object
        secondBackground = Instantiate(gameObject, startPos + new Vector3(repeatWidth, 0, 0), Quaternion.identity);
        secondBackground.GetComponent<RepeatBackground>().enabled = false; // Disable script on duplicate
    }

    void Update()
    {
        // Stop background movement when game over
        if (playerControllerScript.gameOver) 
            return;

        // Move both backgrounds to the left
        transform.position += Vector3.left * speed * Time.deltaTime;
        secondBackground.transform.position += Vector3.left * speed * Time.deltaTime;

        // When the first background moves out of view, shift it in front of the second one
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position += new Vector3(repeatWidth * 2, 0, 0);
        }

        // When the second background moves out of view, shift it in front of the first one
        if (secondBackground.transform.position.x < startPos.x - repeatWidth)
        {
            secondBackground.transform.position += new Vector3(repeatWidth * 2, 0, 0);
        }
    }
}
