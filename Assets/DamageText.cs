using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : PooledObject
{
    public TextMeshPro text;
    private float timer = 0;
    public void Set(Vector3 pos, string text, float timeTillVanish) {
        this.text.SetText(text);
        timer = Time.time + timeTillVanish;
    }

    private void Update() {
         if (Time.time > timer) {
            ReturnToPool();
        }
    }

}
