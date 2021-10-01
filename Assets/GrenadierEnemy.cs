using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadierEnemy : Unit
{
    private void Awake() {
        onDeath += GrenadierDeath;
    }

    public void GrenadierDeath() {
        Destroy(gameObject);
    }
}
