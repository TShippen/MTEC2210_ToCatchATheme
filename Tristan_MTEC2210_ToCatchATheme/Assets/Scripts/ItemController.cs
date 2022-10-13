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

    // used to detect whether or not a coin was collected
    public bool collected = false;
    
    // sound played when coin is collected
    public AudioSource coinAudio;

    // sound played when hazard is collected
    public AudioSource hazardAudio;

    // sound played when hazard is collected, layered
    public AudioSource gameOverSound;


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

        // setting for theme object
        if (gameObject.tag == "Theme")
        {


            if (0f < speedRandomizer && speedRandomizer < 0.5f)
            {
                speed = Random.Range(5, 10) * -1;
            }
            
            else if (0.5f < speedRandomizer && speedRandomizer < 1f) 
            {
                speed = Random.Range(10, 13) * -1;
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
               
        
        // Destory the object if it has not been collected
        // and if the object hits the Ground
        if (other.gameObject.tag == "Ground" && !collected) 
        {
            Destroy(gameObject);
        } 
        
        

        // this applies only to the coin prefab
        // Play collection sound and destroy coin prefab
        if (tag == "Coin" && other.gameObject.tag == "Player") 
        {
            StartCoroutine(FadeAndDestroy(coinAudio, 3f, 1f));
        }

        if (tag == "Hazard" && other.gameObject.tag == "Player") 
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameOverSound.Play();
            StartCoroutine(FadeAndDestroy(hazardAudio, 5f, .3f));


        }

        if (tag == "Theme" && other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Theme Changed");
        }
        
    }

    private IEnumerator FadeAndDestroy(AudioSource audioSource, float duration, float maxVolume)
    {
        // this function first marks the object as "collected", then 
        // destroys the sprite component so it turns invisible.
        // then, the audio volume follows to a sin wave pattern to fade 
        // it in and out, then the object is destroyed.
        // then destroys the
        
        // when this is true, the object wont be destroyed by the 
        collected = true;

        Destroy(spriteRenderer);

        
        float randomStartTime = Random.Range(0, audioSource.clip.length - duration - 1);
        audioSource.time = randomStartTime;
        audioSource.Play();
        audioSource.volume = 0f;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float progress = currentTime/duration;
            progress = Mathf.Sin( Mathf.Lerp(0, Mathf.PI, progress));

            audioSource.volume = Mathf.Clamp(progress, 0, maxVolume);

            yield return null;
        }
        audioSource.Stop();
        Destroy(this.gameObject);
        yield break;
    }
}
