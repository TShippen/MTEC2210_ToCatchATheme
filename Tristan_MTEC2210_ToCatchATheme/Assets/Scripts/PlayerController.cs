using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    public GameManager gameManager;

    public Animator playerAnimator;

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

        // update player animation
        playerAnimator.SetFloat("playerSpeed", xMove);
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
