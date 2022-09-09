using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove =  Input.GetAxisRaw("Horizontal");
        transform.Translate(xMove * speed * Time.deltaTime, 0, 0);
    }
}
