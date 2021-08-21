using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    public string toGoTo;
    public BoxCollider2D collider;
    bool done = false;
    public float timeToScale = 1;
    public AudioSource source;

    public void Awake() {
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == Layers.player) {
            if (!done) {
                RoguelikePlayer r = collision.GetComponent<RoguelikePlayer>();
                r.SetPlayerNextScene(toGoTo);
                collider.enabled = false;
                done = true;
                StartCoroutine(ScaleDown());
                source.Play();

            }
        }

    }

    IEnumerator ScaleDown() {

        float val = timeToScale;

        while (val > 0) {
            transform.localScale = new Vector3(val, val, 1);
            val -= Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(0, 0, 1);



    }

    

}
