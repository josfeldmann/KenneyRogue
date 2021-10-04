using System;
using UnityEngine;



public class Weapon : MonoBehaviour {

    public const int notEquippedlayerorder = 1, equippedLayerorder = 3;
    public bool flipCorrect = false;
    public RoguelikePlayer player;
    public Sprite sprite;
    public Collider2D pickupCollider;
    public SpriteRenderer spriteRenderer;
    public int level = 1;

    [HideInInspector] public AbilityButton button;
    [HideInInspector]public WeaponObject weaponObject;

    public void Set(AbilityButton button, WeaponObject wObject, RoguelikePlayer player) {
        this.button = button;
        this.weaponObject = wObject;
        this.player = player;
    }


    public virtual void SetTargetLayer(LayerMask mask) {

    }

    private void Awake() {
        Setup();
    }

    public virtual void Setup() {

    }

    public virtual void FireDown() {

    }

    public virtual void FireHeld() {
         
    }

    public virtual void FireUp() {

    }

    public virtual string GetDescription() {
        return "Weapon Description";
    }
}
