using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterTargetEvent : MonoBehaviour
{
    public LayerMask targetLayer;

    public UnityEvent onEnter, onExit;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (Layers.InMask(targetLayer, collision.gameObject.layer)) {
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (Layers.InMask(targetLayer, collision.gameObject.layer)) {
            onExit.Invoke();
        }
    }

    public void GoToMapLevel() {
        RoguelikeGameManager.GoToMapLevel();
    }

}
