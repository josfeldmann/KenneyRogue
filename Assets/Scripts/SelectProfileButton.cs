using System;
using TMPro;
using UnityEngine;

public class SelectProfileButton : MonoBehaviour {
    public TextMeshProUGUI text;
    public string profileName;

    public void OnClickProfile() {

    }
    private SaveProfile save;
    internal void SetProfile(SaveProfile saveProfile) {
        save = saveProfile;
        text.SetText(saveProfile.profileName);
    }

    public void PickProfile() {
        SaveUtility.SetLastProfile(save.profileName);
        RoguelikeGameManager.SetProfile(save);
        ModeSelector.instance.SetMainMenu();
    }
}
