using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject itemPrefab;
    public Transform leftTran;
    public Transform rightTran;

    private float spawnDelay = 1;
    private float spawnRate = 2;


    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating(nameof(SpawnItem), spawnDelay, spawnRate);
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnItem()
    {
        float randomXValue = Random.Range(leftTran.position.x, rightTran.position.x);

        Vector2 spawnPos = new Vector2(randomXValue, leftTran.position.y);

        Instantiate(itemPrefab, spawnPos, Quaternion.identity);
    }
}
