using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggler : MonoBehaviour
{
    public float wiggleAmount = 15f;
    public float wiggleSpeed = 180f;
    private float wiggleTarget;
    public float currentWiggle = 0;
    public bool wiggling = false;


    public void StartWiggle() {
        wiggling = true;
        
        if (Random.Range(0f,1f) > 0.5f) {
            wiggleTarget = wiggleAmount;
        } else {
            wiggleTarget = -wiggleAmount;
        }
    }

    public void StopWiggle() {
        wiggleTarget = 0;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void Update() {
        if (wiggling) {
            if (currentWiggle == wiggleTarget) {
                wiggleTarget = -wiggleTarget;
            } else {
                currentWiggle = Mathf.MoveTowards( currentWiggle, wiggleTarget, wiggleSpeed * Time.deltaTime);
                transform.localEulerAngles = new Vector3(0, 0, currentWiggle);
            }
        } else if (currentWiggle > 0) {
            currentWiggle = Mathf.MoveTowards(transform.eulerAngles.z, 0, wiggleSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, currentWiggle);
        }
    }


}
