using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource currentAudioSource;
    public AudioSource source1, source2;



    private void Awake() {
        if (instance == null) {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        } else {
            if (this != instance) {
                Destroy(gameObject);
            }
        }
    }

    public static void PlayTrack(AudioClip c) {
        
      
        instance.PTrack(c);
    }


    private void PTrack(AudioClip c) {


        if (currentAudioSource == null) {
            currentAudioSource = source1;
            currentAudioSource.clip = c;
            currentAudioSource.Play();
            return;
        } else if (currentAudioSource.clip == c) {
            return;
        } else {
            StartCoroutine(SwitchTracks(c));
        }



    }


   
    public IEnumerator SwitchTracks(AudioClip clip) {

        AudioSource oldSource = null;


        if (currentAudioSource == source1) {

            oldSource = source1;
            currentAudioSource = source2;
        } else {
            oldSource = source2;
            currentAudioSource = source1;
        }

        float val = 0;
        currentAudioSource.clip = clip;
        currentAudioSource.volume = 1;
        oldSource.volume = 0;
        currentAudioSource.Play();

        while (val < 1) {
            val += Time.deltaTime;
            currentAudioSource.volume = val;
            oldSource.volume = 1 - val;
            yield return null;
        }

        currentAudioSource.volume = 1;
        oldSource.volume = 0;

        
    }


}
