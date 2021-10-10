using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHoverBehaviour : HoverBehaviour {

    public UnityEvent OnClickEvent;

    public override void OnHoverEnter() {
        
    }

    public override void OnHoverExit() {
        
    }

    public override void OnClick() {
        OnClickEvent.Invoke();
    }
}
