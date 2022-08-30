using UnityEngine;

public class MapPlayer : MonoBehaviour {
    public SpriteRenderer sprite;
    public Wiggler wiggler;


    private void Awake() {
        wiggler.StartWiggle();
    }
}
