using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoguelikeGameManager 
{
    
    public static string currentSeed = null;
    public static SaveProfile currentProfile;



    //RunData;
    public static RoguelikePlayer player;

    public static void SetPlayer(RoguelikePlayer p) {
        player = p;
    }

    internal static void SetProfile(SaveProfile s) {
        currentProfile = s;
        currentSeed = s.currentSeed;
        SaveUtility.SaveProfile(s);
    }

    internal static void GoToMapLevel() {
        SceneManager.LoadScene(SceneNames.MapLevel);
    }
}
