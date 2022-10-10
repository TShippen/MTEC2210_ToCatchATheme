using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    public GameObject [] itemPrefab;
    public Transform leftTran;
    public Transform rightTran;
    public Transform bottomTrans;

    public TextMeshPro scoreText;
 
    public int score;


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
        scoreText.text = "Score: " + score.ToString();

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


   

    public static IEnumerator FadeInOut(AudioSource audioSource, float duration, float maxVolume)
    {
        float randomStartTime = Random.Range(0, audioSource.clip.length - duration - 1);
        audioSource.time = randomStartTime;
        Debug.Log(audioSource.time);
        audioSource.Play();
        audioSource.volume = 0f;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float progress = currentTime/duration;
            progress = Mathf.Lerp(0, Mathf.PI, progress);
            progress = Mathf.Sin(progress);

            audioSource.volume = Mathf.Clamp(progress, 0, maxVolume);
            Debug.Log(audioSource.volume);



            yield return null;
        }
        audioSource.Stop();
        yield break;
    }

}
