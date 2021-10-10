using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour {

   // public InputActionAsset inputAsset;
  //  public InputAction weaponAction, skillOneAction, skillTwoAction, skillThreeAction, escAction;
    


    public float horizontal, vertical;
    public bool jumpPressed, jumpReleased,  firePressed, fireReleased, fireheld, pickupButton;
    public bool pauseButtonDown = false, resetButtonDown = false;

    public bool skill1Up, skill1Down, skill1Held;

    public bool skill2Up, skill2Down, skill2Held;

    public bool skill3Up, skill3Down, skill3Held;


    private void Awake() {
     //   weaponAction = inputAsset.FindAction("Weapon");
     //   escAction = inputAsset.FindAction("Pause");
    }

    private void Update() {
        // horizontal = Input.GetAxis("Horizontal");
        //    vertical = Input.GetAxis("Vertical");
        //  jumpPressed = Input.GetKeyDown(KeyCode.Space);
        //   jumpReleased = Input.GetKeyUp(KeyCode.Space);
        //  firePressed = weaponAction.WasPressedThisFrame();
        //  fireReleased = weaponAction.WasReleasedThisFrame();
        //  fireheld = weaponAction.IsPressed();
        //  pickupButton = Input.GetKeyDown(KeyCode.E);
        //  pauseButtonDown = escAction.WasPressedThisFrame();
        // resetButtonDown = Input.GetKeyDown(KeyCode.R);


        //  skill1Up = Input.GetMouseButtonUp(1);
        //   skill1Down = Input.GetMouseButtonDown(1);
        // skill1Held = Input.GetMouseButton(1);

        //  skill2Up = Input.GetKeyUp(KeyCode.LeftShift);
        //  skill2Down = Input.GetKeyDown(KeyCode.LeftShift);
        // skill2Held = Input.GetKey(KeyCode.LeftShift);

        //  skill3Up = Input.GetKeyUp(KeyCode.Space);
        //  skill3Down = Input.GetKeyDown(KeyCode.Space);
        //  skill3Held = Input.GetKey(KeyCode.Space);

        //  firePressed = false;
        //  fireReleased = false;

       
    }

    public void LateUpdate() {
        fireReleased = false;
        firePressed = false;
        skill1Up = false;
        skill1Down = false;
        skill2Up = false;
        skill2Down = false;
        skill3Down = false;
        skill3Up = false;
        pauseButtonDown = false;
    }

    public void OnWeapon(CallbackContext context) {
        //print("Here " + Time.time);
        if (context.started) {
            firePressed = true;
            fireheld = true;
            fireReleased = false;
        } else if (context.canceled) {
            firePressed = false;
            fireheld = false;
            fireReleased = true;
        } 
    }


    public void OnSkillTwo(CallbackContext context) {
     //   print("Here " + Time.time);
        if (context.started) {
            skill2Down = true;
            skill2Held = true;
            skill2Up = false;
        } else if (context.canceled) {
            skill2Down = false;
            skill2Held = false;
            skill2Up = true;
        }
    }




    public void OnSkillOne(CallbackContext context) {
       // print("Here " + Time.time);
        if (context.started) {
            skill1Down = true;
            skill1Held = true;
            skill1Up = false;
        } else if (context.canceled) {
            skill1Down = false;
            skill1Held = false;
            skill1Up = true;
        }
    }



    public void OnSkillThree(CallbackContext context) {
      //  print("Here " + Time.time);
        if (context.started) {
            skill3Down = true;
            skill3Held = true;
            skill3Up = false;
        } else if (context.canceled) {
            skill3Down = false;
            skill3Held = false;
            skill3Up = true;
        }
    }

    Vector2 vec;

    public void OnMove(CallbackContext context) {
        vec = context.ReadValue<Vector2>();
        horizontal = vec.x;
        vertical = vec.y;
    }

    public void OnPause(CallbackContext context) {
        if (context.started) {
            pauseButtonDown = true;
        }
    }
}
