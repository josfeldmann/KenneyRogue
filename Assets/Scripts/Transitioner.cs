using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transitioner : MonoBehaviour
{
    public Image transitionImage;

    public IEnumerator Transition(float start, float end) {
        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.g, start);
        transitionImage.enabled = true;
        float val = start;

        while (val != end) {
            val = Mathf.MoveTowards(val, end, Time.deltaTime);
            transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.g, val);
            yield return null;
        }
        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.g, end);


    }


    public void IntroTransition() {
        StartCoroutine(Transition(1, 0));
    }

    public void EndTransition() {
        StartCoroutine(Transition(0, 1));
    }


}
