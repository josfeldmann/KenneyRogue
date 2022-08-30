using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Unit
{
    private void Awake() {
        onDeath += Break;
    }

    public void Break() {
        Destroy(gameObject);
    }
}
