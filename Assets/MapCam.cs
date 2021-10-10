using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCam : MonoBehaviour
{

    public static Camera current;
    public Camera cam;

    private void Awake() {
        current = cam;
    }
}
