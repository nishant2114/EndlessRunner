using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SpawnManager2 : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array to store different obstacles
    private float minX = 20f;  // Min X position for spawning obstacles
    private float maxX = 40f;  // Max X position for spawning obstacles
    private float[] lanes = { -8f, 0f, 8f }; // Possible Z positions (lanes)

    private float startDelay = 1f;
    private float spawnInterval = 1.5f;
    private int maxObstaclesOnScreen = 5;
    
    public TextMeshProUGUI scoreText;
    private int score;

    private PlayerController playerControllerScript;
    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        UpdateScore(0);
    }

    void Update()
    {
        // Remove destroyed obstacles from the list
        activeObstacles.RemoveAll(item => item == null);
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver || activeObstacles.Count >= maxObstaclesOnScreen)
            return;

        int obstaclesToSpawn = Random.Range(1, 4); // Spawn 1 to 3 obstacles randomly
        List<float> usedLanes = new List<float>(); // Track used lanes to avoid repetition

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            float randomX = Random.Range(minX, maxX); // Random X position
            float randomZ;

            // Ensure obstacles don't always spawn on the same lanes
            do
            {
                randomZ = lanes[Random.Range(0, lanes.Length)];
            } while (usedLanes.Contains(randomZ));

            usedLanes.Add(randomZ); // Mark this lane as used

            Vector3 spawnPos = new Vector3(randomX, 0, randomZ);
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            
            activeObstacles.Add(newObstacle);
            UpdateScore(5);
        }

        
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
