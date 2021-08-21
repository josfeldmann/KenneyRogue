using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    public float amt = 90f;

    private void FixedUpdate() {
        transform.Rotate(0, 0, amt * Time.fixedDeltaTime);
    }

}
