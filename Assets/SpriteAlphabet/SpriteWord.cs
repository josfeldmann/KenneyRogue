using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWord : MonoBehaviour {


    public const float pixelIncrement = 0.06250f;
    public TextAlignment alignment = TextAlignment.Left;
    public string setString = "123";
    public static SpriteRenderer rendererprefab;
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    private void Start() {
        SetWord();
    }

    [ContextMenu("SetWord")]
    public void SetWord() {
        SetWord(setString);
    }


    public void SetWord(string s) {
        if (rendererprefab == null) {
            rendererprefab = Resources.Load<GameObject>("CharSpritePrefab").GetComponent<SpriteRenderer>();
        }
        if (GameDatabase.charSprite == null || GameDatabase.charSprite.Count == 0) {
            GameDatabase.LoadAlphabet(Resources.Load<SpriteAlphabet>("SpriteAlphabet"));
        }
        setString = s;
        while (renderers.Count < s.Length) {
            renderers.Add(Instantiate(rendererprefab, transform));
        }

        SpriteRenderer lastRenderer = renderers[0];

        for (int i = 0; i < renderers.Count; i++) {
            if (i < s.Length) {
                renderers[i].gameObject.SetActive(true);
                renderers[i].sprite = GameDatabase.charSprite[char.ToLower(s[i])];

               
                        if (i > 0) {
                            float offset = ((renderers[i].sprite.rect.width / 2) + (renderers[i - 1].sprite.rect.width / 2)) * pixelIncrement;
                            renderers[i].transform.localPosition = renderers[i - 1].transform.localPosition + new Vector3(offset - pixelIncrement, 0, 0);
                    lastRenderer = renderers[i];
                        } else {
                            renderers[i].transform.localPosition = new Vector3((renderers[i].sprite.rect.width / 2) * pixelIncrement, 0, 0);
                        }
                      
                    
            } else {
                renderers[i].gameObject.SetActive(false);
            }
        }

        if (alignment == TextAlignment.Center) {
            float width =   (lastRenderer.transform.localPosition.x + ((lastRenderer.sprite.rect.width / 2) * pixelIncrement)) -  (renderers[0].transform.localPosition.x - ((renderers[0].sprite.rect.width / 2) * pixelIncrement));
            width /= 2;
            foreach (SpriteRenderer sr in renderers) {
                sr.transform.localPosition = sr.transform.localPosition - new Vector3(width, 0, 0);
            }
        }

        
    }


}

