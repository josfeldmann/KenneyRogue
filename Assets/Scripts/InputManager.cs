using UnityEngine;

public class InputManager : MonoBehaviour {

    public float horizontal, vertical;
    public bool jumpPressed, jumpReleased,  firePressed, fireReleased, fireheld, pickupButton;
    public bool pauseButtonDown = false;


    private void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        jumpReleased = Input.GetKeyUp(KeyCode.Space);
        firePressed = Input.GetMouseButtonDown(0);
        fireReleased = Input.GetMouseButtonUp(0);
        fireheld = Input.GetMouseButton(0);
        pickupButton = Input.GetKeyDown(KeyCode.E);
        pauseButtonDown = Input.GetKeyDown(KeyCode.Escape);
    }


}
