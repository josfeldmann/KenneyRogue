using UnityEngine;



public class Weapon : MonoBehaviour {

    public const int notEquippedlayerorder = 1, equippedLayerorder = 3;
    public bool flipCorrect = false;
    public RoguelikePlayer player;
    public Sprite sprite;
    public Collider2D pickupCollider;
    public SpriteRenderer spriteRenderer;

    private void Awake() {
       
    }

    public virtual void FireDown() {

    }

    public virtual void FireHeld() {
         
    }

    public virtual void FireUp() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.player) {
            RoguelikePlayer r = collision.GetComponent<RoguelikePlayer>();
            
            if (r.currentNumberOfWeapons < r.numberOfWeapons) {
                r.EquipWeapon(this);
            } else {
                r.HoveredWeapons.Add(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.player) {
            RoguelikePlayer r = collision.GetComponent<RoguelikePlayer>();
            if (r.HoveredWeapons.Contains(this))r.HoveredWeapons.Remove(this);
        }
    }

}
