using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    public string toGoTo;
    public BoxCollider2D collider;
    bool done = false;
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == Layers.player) {
            if (!done) {
                RoguelikePlayer r = collision.GetComponent<RoguelikePlayer>();
                r.SetPlayerNextScene(toGoTo);
                collider.enabled = false;
                done = true;
            }
        }

    }

    

}
