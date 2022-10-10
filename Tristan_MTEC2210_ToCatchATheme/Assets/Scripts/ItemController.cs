using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Animator is used for the hazard objects
    public Animator animator;

    // SpriteRenderer is used for the coin objects
    public SpriteRenderer spriteRenderer;

    // Sprite array used to randomize sprites
    public Sprite [] coinIcon;

    // object movement speed
    public float speed; 
    
    

    // Start is called before the first frame update
    void Start()
    {
        // randomize the sprite chosen from coinIcon
        float spriteRandomizer = Random.value;

        

        // randomize the speed within more specific ranges
        float speedRandomizer = Random.value;

        // settings for hazard object
        if (gameObject.tag == "Hazard")
        {
            // set animation
            if (0f < spriteRandomizer && spriteRandomizer < 0.3f)
            {
                animator.Play("Hazard_1");
            }

            else if (0.4f < spriteRandomizer && spriteRandomizer < 0.6f)
            {
                animator.Play("Hazard_2");
            }

            else if (0.7f < spriteRandomizer && spriteRandomizer < 1f)
            {
                animator.Play("Hazard_3");
            }
            
            // set speed
            if (0f < speedRandomizer && speedRandomizer < 0.5f)
            {
                speed = Random.Range(3, 5) * -1;
            }
            
            else if (0.5f < speedRandomizer && speedRandomizer < 1f) 
            {
                speed = Random.Range(6, 10) * -1;
            }
        }

        // setting for coin object
        if (gameObject.tag == "Coin")
        {

            if (0f < spriteRandomizer && spriteRandomizer < 0.5f) 
            {
                spriteRenderer.sprite = coinIcon[0];
            }

            if (0.5f < spriteRandomizer && spriteRandomizer < 1f) 
            {
                spriteRenderer.sprite = coinIcon[1];

            }

            if (0f < speedRandomizer && speedRandomizer < 0.5f)
            {
                speed = Random.Range(5, 10) * -1;
            }
            
            else if (0.5f < speedRandomizer && speedRandomizer < 1f) 
            {
                speed = Random.Range(10, 15) * -1;
            }
        }
         
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the object
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    { 
        // Destory the object if it colides with the ground
        if (other.gameObject.tag == "Ground") 
        {
            Debug.Log("ground");
            Destroy(gameObject);
        } 
    }
}
