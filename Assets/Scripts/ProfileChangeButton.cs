using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileChangeButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetProfile(SaveProfile s) {
        text.text = s.profileName;
    }

    public void ChangeProfile() {
        ModeSelector.instance.ShowProfileSelectMenu();
    }


}
