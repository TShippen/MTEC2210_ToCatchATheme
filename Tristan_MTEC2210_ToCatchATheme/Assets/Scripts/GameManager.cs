using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject [] itemPrefab;
    public Transform leftTran;
    public Transform rightTran;
    public Transform bottomTrans;

    public TextMeshPro scoreText;
 
    public int score;

    private AudioSource audioSource;

    private float spawnDelay = 1;
    private float spawnRate = 2;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
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


    public void PlaySound(AudioClip audioClip) 
    {
        audioSource.PlayOneShot(audioClip);
    }

    public static IEnumerator FadeInOut(AudioSource audioSource, float durationIn, float durationHold, float durationOut, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
