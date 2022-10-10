using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    public GameManager gameManager;
    public AudioClip coinAudio;
    public AudioClip hazardAudio;

    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        // set player object speed
        speed = 6;
    }

    // Update is called once per frame
    void Update()
    {
        // get keyboard input
        float xMove =  Input.GetAxisRaw("Horizontal");

        //move the player object
        transform.Translate(xMove * speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        // Destory the coin items if it colides with the player
        if (other.gameObject.tag == "Coin") {

            
            gameManager.IncrementScore(1);
            Destroy(other.gameObject);
            
        }
        
        // Destory the player if it colides with an enemy
        if (other.gameObject.tag == "Hazard") {

            Destroy(gameObject);
        }
        
    }
}
