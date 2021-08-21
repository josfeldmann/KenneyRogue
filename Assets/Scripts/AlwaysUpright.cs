using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysUpright : MonoBehaviour
{
    public bool startingPos = false;

    Quaternion rot = Quaternion.identity;

    private void Awake() {
        if (startingPos) rot = transform.rotation;
        else rot = Quaternion.identity;
    }


    private void Update() {
     //   transform.rotation = Quaternion.identity;
    }

    private void LateUpdate() {
        transform.rotation = rot;
    }

    private void FixedUpdate() {
      //  transform.rotation = Quaternion.identity;
    }
}
