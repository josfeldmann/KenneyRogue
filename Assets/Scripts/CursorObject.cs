using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{
    public Sprite cursorSprite;
    public static CursorObject instance;
    public SpriteRenderer spriteRenderer;
    public Camera cam;

    private void Awake() {
        instance = this;
        Cursor.visible = false;
    }

    public static void SetSprite(Sprite s) {
        instance.spriteRenderer.sprite = s;
    }

    public static void SetDefaultSprite() {
        instance.spriteRenderer.sprite = instance.cursorSprite;
    }

    private void Update() {
       transform.position =  cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

    }

}
