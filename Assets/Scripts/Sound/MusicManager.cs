using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource start;
    public AudioSource wind;
    public float windVolume = 0.5f;
    public AudioClip[] layers;
    private List<AudioSource> layerSources;

    public int currentLayer = 1;
    public float bpm = 104;
    private float loopTime;
    
    [Range(0, 1)]
    public float musicVolume = 0.6f;

    void Awake()
    {
        wind.volume = windVolume;
        loopTime = 120/bpm*16;

        layerSources = new List<AudioSource>();
        foreach (AudioClip layer in layers) {
            AudioSource layerSource = gameObject.AddComponent<AudioSource>();
            layerSource.clip = layer;
            layerSource.volume = musicVolume;
            layerSources.Add(layerSource);
        }
    }

    public void playStart() {
        start.Play();
    }

    public IEnumerator windFade(float duration)
    {
        float currentTime = 0;
        float start = wind.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            wind.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public IEnumerator playMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        while(true)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        {
            for (int i=0; i<currentLayer; i++) {
                layerSources[i].volume = musicVolume; // warning : possible conflict with restart music
                layerSources[i].Play();
            }
            yield return new WaitForSeconds(loopTime);
            musicEvolve();
        }
    }

    public IEnumerator restartMusic(float duration)
    {
        float currentTime = 0;
        float start = musicVolume;
        currentLayer = 1;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            for (int i=0; i<layerSources.Count-1; i++) {
                layerSources[i+1].volume = Mathf.Lerp(start, 0, currentTime / duration);
            }
            yield return null;
        }
        yield break;
    }

    public void musicEvolve() {
        if (currentLayer < layerSources.Count) {
            currentLayer++;
            if (currentLayer == 4) currentLayer++; // percusions are played with climax
        } else {
            currentLayer--; // lead loop is two times longer
        }
    }
}
