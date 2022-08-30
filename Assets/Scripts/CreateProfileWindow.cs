using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateProfileWindow : MonoBehaviour
{
    public TMPro.TMP_InputField input;
    public Button createButton, cancelButton;

    public void Setup(bool canCancel) {
        input.SetTextWithoutNotify("");
        createButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(canCancel);
    }


    public void CheckInput() {
        bool canSubmit = false;
        input.text = input.text.RemoveSpecialCharacters();
        if (input.text != null && input.text != "") {
            canSubmit = true;
        }
        createButton.gameObject.SetActive(canSubmit);
    }

    public void CreateProfile() {
        SaveProfile s =  SaveUtility.CreateNewProfile(input.text);
        RoguelikeGameManager.SetProfile(s);
        ModeSelector.instance.SetMainMenu();
    }

}
