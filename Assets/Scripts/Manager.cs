using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    
    static int gameTime1;
    static int gameTime2;
    static int boost;
    static bool isNotPaused = true;
    public GameObject prefab;
    float height = 0.34f;
    int placement;

    // Wave speed
    public WaveManager wave;
    private float initialWaveSpeed;
    public float waveAcceleration = 0.05f;
    private float startDeccelerate;

    // Music
    public MusicManager musicManager;

    // Start is called before the first frame update
    void Start()
    {
        isNotPaused = false;
        initialWaveSpeed = wave.speed;
    }

    void FixedUpdate()
    {
        if(isNotPaused){
        gameTime1 +=1;
        gameTime2 +=1;
        //every 2 seconds, increase the speed of the stones by 0.1f
        if (gameTime1>120 && StoneMovement.speed < 10f)
        {
            StoneMovement.speed += 0.1f;
            wave.speed+=waveAcceleration;
            gameTime1 = 0;
        }
        
        //create instances of prefab every x seconds (x = 5 - gameTime)
        if (gameTime2 > (250-boost) )
        {
            placement = Random.Range(1, 4);
            if(placement == 1)
            {
                Instantiate(prefab, new Vector3(22, height, 2.5f), Quaternion.identity);
                Instantiate(prefab, new Vector3(22, height, 0), Quaternion.identity);
            }
            if(placement == 2)
            {
                Instantiate(prefab, new Vector3(22, height, -2.5f), Quaternion.identity);
                Instantiate(prefab, new Vector3(22, height, 2.5f), Quaternion.identity);
            }
            if(placement == 3)
            {
                Instantiate(prefab, new Vector3(22, height, -2.5f), Quaternion.identity);
                Instantiate(prefab, new Vector3(22, height, 0), Quaternion.identity);
            }
            if (boost <= 120) boost+=10;
            gameTime2 = 0;        
        }
        }
    }
    public void StStart()
    {
        gameTime1 = 0;
        gameTime2 = 250;
        boost = 0;
        StoneMovement.speed = 2f;
        startDeccelerate = wave.speed;
        isNotPaused = true;
    }
    public void StPause()
    {
        isNotPaused = false;
    }

    public IEnumerator slowWave(float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            wave.speed = Mathf.Lerp(startDeccelerate, 0, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
