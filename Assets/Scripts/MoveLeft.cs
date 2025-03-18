using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15;
    private PlayerController playerControllerScript;
    private float leftbound = -15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.gameOver == false){
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        }

        if( transform.position.x < leftbound && gameObject.CompareTag("Obstacle")){
            Destroy(gameObject);
        }

    }
}
