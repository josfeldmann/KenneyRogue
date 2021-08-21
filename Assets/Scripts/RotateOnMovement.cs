using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnMovement : MonoBehaviour
{
    public float rotateAmount = 15f;
    public InputManager manager;


    private void Update() {

        if (manager.horizontal != 0) {
            transform.Rotate(0, 0, rotateAmount * manager.horizontal * Time.deltaTime);
        }

        if (manager.vertical != 0) {
            if (manager.horizontal != 0) {
                transform.Rotate(0, 0, rotateAmount * -manager.vertical * Time.deltaTime);
            }
        }

    }

}
