using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatTextIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public StatEnum statEnum;
    public Image statIconImage;
    public TextMeshProUGUI statText;

    public void OnPointerEnter(PointerEventData eventData) {
        TooltipMaster.current.SetStat(statEnum, this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        TooltipMaster.current.CloseToolTip();
    }

    public void Set(float stat) {
        statText.text = ((int)stat).ToString();
    }

}
