using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldHoverCaster : MonoBehaviour
{
    public InputManager InputManager;
    public LayerMask layerMask;
    public GameObject cache;
    public HoverBehaviour currentbehaviour;
    RaycastHit2D hit;
    private void Update() {
        if (MapCam.current == null) return;
        hit = Physics2D.Raycast(MapCam.current.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector3.forward, 1, layerMask);
        if (hit) {
            if (cache != hit.collider.gameObject) {
                if (currentbehaviour != null) currentbehaviour.OnHoverExit();
                cache = hit.collider.gameObject;
                currentbehaviour = cache.GetComponent<HoverBehaviour>();
                currentbehaviour.OnHoverEnter();
            }
        } else {
            if (cache != null) {
                cache = null;
                if (currentbehaviour != null) {
                    currentbehaviour.OnHoverExit();
                }
                currentbehaviour = null;
                
            }
        }
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            if (currentbehaviour != null) {
                currentbehaviour.OnClick();
            }
        }

    }
}


public abstract class HoverBehaviour : MonoBehaviour {
    public abstract void OnHoverEnter();

    public abstract void OnHoverExit();

    public virtual void OnClick() {

    }

}
