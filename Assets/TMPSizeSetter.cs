using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPSizeSetter : MonoBehaviour
{
    public float lineSize = 40;
    public TextMeshProUGUI text;
    public RectTransform rect;
    public RectTransform parentWidthReference;


    [ContextMenu("Test")]
    public void Test() {
        text.SetText("aaaaaa\naaaa\naaa\n");
        text.ForceMeshUpdate();
        rect.sizeDelta = new Vector2(parentWidthReference.rect.width, text.textInfo.lineCount * lineSize);
    }

    public void SetText(string s) {
        text.SetText(s);
        text.ForceMeshUpdate();
        rect.sizeDelta = new Vector2(parentWidthReference.rect.width, text.textInfo.lineCount * lineSize);
    }
}
