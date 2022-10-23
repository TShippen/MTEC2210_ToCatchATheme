using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    public GameManager gameManager;

    public Animator playerAnimator;

    private Rigidbody2D rigidbody2D;

    public float speed;

    public float jumpSpeed;

    private float jumpOrigin;
    
    private bool jumping = false;



    

    // Start is called before the first frame update
    void Start()
    {
        // set player object speed
        speed = 200;

        jumpSpeed = 5;

        // get player object ridgedbody2D component
        rigidbody2D = GetComponent<Rigidbody2D>();

        jumpOrigin = transform.position.y;


        
    }

    // Update is called once per frame
    void Update()
    {
        // movement code that uses GetAxis
        /*
        // get keyboard input
        float xMove =  Input.GetAxisRaw("Horizontal");
        
        //move the player object
        transform.Translate(xMove * speed * Time.deltaTime, 0, 0);
        */

        if (Input.GetKeyDown(KeyCode.Space) && jumpOrigin > transform.position.y)
        {
            jumping = true;
        }

        


        
    }

    void FixedUpdate()
    {
        // move character to the left
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-speed * Time.deltaTime, rigidbody2D.velocity.y);
            
            // update player animation
            playerAnimator.SetFloat("playerSpeed", -1);
        }

        // move character to the left
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(speed * Time.deltaTime, rigidbody2D.velocity.y);
            
            // update player animation
            playerAnimator.SetFloat("playerSpeed", 1);
        }

        if (jumping == true) 
        {
            rigidbody2D.velocity = Vector2.up * jumpSpeed;
            jumping = false;
        }


    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        
        // increment the score if the player colides with a coin
        if (other.gameObject.tag == "Coin") {

            gameManager.IncrementScore(1);
            playerAnimator.SetBool("collectedCoin", true);
            StartCoroutine(StartAnimationWaitFinish(playerAnimator, "Player_collect_coin", "collectedCoin"));
            
        }
                
    }

    private IEnumerator StartAnimationWaitFinish(Animator animator, string animationName, string transitionParameterName) 
    {
        animator.Play(animationName);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);
        animator.SetBool("collectedCoin", false);
        
    }
}
