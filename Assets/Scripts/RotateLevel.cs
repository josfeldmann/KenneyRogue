using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevel : MonoBehaviour
{

    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayTrack(clip);
    }

   
}
