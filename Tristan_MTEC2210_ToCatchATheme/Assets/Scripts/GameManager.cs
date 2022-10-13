using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    public GameObject [] itemPrefab;
    public Transform leftTran;
    public Transform rightTran;

    public AudioSource backgroundMusic;
    public TextMeshPro scoreText;
 
    public int score;

    private float spawnDelay = 1;
    private float spawnRate = 2;

    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        // find the player object, activate, and set starting coordinates
        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.transform.position = new Vector3(0, -2.5f, 0);

        // set score to 0
        score = 0;

        // start background music
        backgroundMusic.Play();
        
        // start spawning items
        InvokeRepeating(nameof(SpawnItem), spawnDelay, spawnRate);
        

        
    }

    // Update is called once per frame
    void Update()
    {
        // keep score updated
        scoreText.text = "Score: " + score.ToString();

        
        

        if (!player.GetComponent<SpriteRenderer>().enabled) 
        {
            GameOverAndReset();

        }
        
             


    }

    public void SpawnItem()
    {
        float randomXValue = Random.Range(leftTran.position.x, rightTran.position.x);

        Vector2 spawnPos = new Vector2(randomXValue, leftTran.position.y);

        int index = Random.Range(0, itemPrefab.Length); 
        Instantiate(itemPrefab[index], spawnPos, Quaternion.identity);
    }

    public void IncrementScore(int value) 
    {
        score += value;
    }

    private void GameOverAndReset() 
    {
        // stop spawning new items
        CancelInvoke(nameof(SpawnItem));
        
        // stop the background music        
        backgroundMusic.Stop();
            
        // set score text to "Final Score"
        scoreText.text = "Final Score: " + score.ToString();

        // if player presses r, reset the game by calling start() 
        if (Input.GetKeyDown("r")) 
            {
                Start();
            }
    }


}
