using UnityEngine;

public class InputManager : MonoBehaviour {

    public float horizontal, vertical;
    public bool jumpPressed, jumpReleased,  firePressed, fireReleased, fireheld, pickupButton;
    public bool pauseButtonDown = false, resetButtonDown = false;

    public bool skill1Up, skill1Down, skill1Held;

    public bool skill2Up, skill2Down, skill2Held;

    public bool skill3Up, skill3Down, skill3Held;


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
        resetButtonDown = Input.GetKeyDown(KeyCode.R);


        skill1Up = Input.GetMouseButtonUp(1);
        skill1Down = Input.GetMouseButtonDown(1);
        skill1Held = Input.GetMouseButton(1);

        skill2Up = Input.GetKeyUp(KeyCode.LeftShift);
        skill2Down = Input.GetKeyDown(KeyCode.LeftShift);
        skill2Held = Input.GetKey(KeyCode.LeftShift);

        skill3Up = Input.GetKeyUp(KeyCode.Space);
        skill3Down = Input.GetKeyDown(KeyCode.Space);
        skill3Held = Input.GetKey(KeyCode.Space);

    }


}
