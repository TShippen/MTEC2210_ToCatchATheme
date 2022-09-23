using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float speed; // object movement speed
    
    

    // Start is called before the first frame update
    void Start()
    {

        // Randomize the speed within more specific ranges
        float speedRandomizer = Random.value;

        // Most likely speed range is average speed, between 6 and 11
        if (0f < speedRandomizer && speedRandomizer < 0.5f)
        {
            speed = Random.Range(6, 11) * -1;
        }
        // Second most like speed range is slow, betwwen 3 and 5 
        else if (0.5f < speedRandomizer && speedRandomizer < 0.8f) 
        {
            speed = Random.Range(3, 5) * -1;
        }
        // Least likely speed range is fast, betwwen 12 and 15 
        else if (0.8f < speedRandomizer && speedRandomizer < 1f) 
        {
            speed = Random.Range(12, 15) * -1;
        }
         
        
    }

    // Update is called once per frame
    void Update()
    {
        

        // move the object
        transform.Translate(0, speed * Time.deltaTime, 0);
        
        

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        // Destory the object if it colides with the ground
        if (other.gameObject.tag == "Ground") 
        {
            Debug.Log("ground");
            Destroy(gameObject);
        }
        
        
    }


}
