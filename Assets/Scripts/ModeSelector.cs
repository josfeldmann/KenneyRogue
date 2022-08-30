using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelector : MonoBehaviour {

    public static ModeSelector instance;

    public PlayerPartDataBase playerDataBase;


    [Header("Run Selector")]
    public TextMeshPro seedText;
    public GameObject runSelector;
    public ModeSelectorCursor cursor;
    public RoguelikePlayer player;
    public RunSelectFigure[] runFigures = new RunSelectFigure[4];
    public RunSelectFigure dailyRun, customRun, questRun;



    public GameObject MainMenu;
    public GameObject continueButton;
    

    public QuestWindow questWindow;


    [Header("Profile Selection")]
    public ProfileChangeButton profileChangeButton;
    public ProfileChangeWindow changeWindow;
    public CreateProfileWindow createProfileWindow;

    private void Awake() {
         foreach (RunSelectFigure f in runFigures) {
             f.modeSelector = this;
         }
         dailyRun.modeSelector = this;
         customRun.modeSelector = this;
         questRun.modeSelector = this;
        instance = this;
        if (RoguelikeGameManager.player != null) {
            Destroy(player);
        } else {
            GameObject.DontDestroyOnLoad(player.gameObject);
            RoguelikeGameManager.player = player;
            
        }
        player.DisablePlayerWithoutUI();
        SaveProfile s = SaveUtility.GetLastSavedProfile();

        if (s == null) {
            List<SaveProfile> saved = SaveUtility.GetSaveProfiles();
            if (saved.Count > 0) {
                ShowProfileSelectMenu();
            } else {
                ShowCreateProfileWindow(false);
            }
        } else {
            RoguelikeGameManager.SetProfile(s);
            SetMainMenu();
        }


    }

    internal void StartNormalRunWith(RunSelectFigure runSelectFigure) {

        HideAll();
        player.raceObject = runSelectFigure.race;
        player.weaponObject = runSelectFigure.weaponObject;
        player.transform.position = runSelectFigure.transform.position;
        List<AbilityObject> abilitiesToAdd = new List<AbilityObject>(runSelectFigure.abilities);
        abilitiesToAdd.Insert(0,  runSelectFigure.race.racialAbility);
        player.items = new List<ItemObject>();
        player.abilityObjects = abilitiesToAdd;
        player.Setup();


    }

    public void HideAll() {
        runSelector.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        profileChangeButton.gameObject.SetActive(false);
        changeWindow.gameObject.SetActive(false);
        createProfileWindow.gameObject.SetActive(false);
    }

    internal void SetMainMenu() {
        HideAll();
        MainMenu.gameObject.SetActive(true);
        profileChangeButton.gameObject.SetActive(true);
        profileChangeButton.SetProfile(RoguelikeGameManager.currentProfile);
        
       
    }

    internal void Hover(RunSelectFigure runSelectFigure) {
        cursor.gameObject.SetActive(true);
        cursor.gameObject.transform.position = runSelectFigure.transform.position;
    }

    internal void HoverExit(RunSelectFigure runSelectFigure) {

    }

    public void GenerateRunners(RunSelectFigure f) {

    }

    public void ShowRunMenu() {

        UnityEngine.Random.InitState(RoguelikeGameManager.currentSeed.GetHashCode());

        seedText.text = RoguelikeGameManager.currentSeed;

        List<AbilityObject> l = new List<AbilityObject>();
        foreach (AbilityObject a in playerDataBase.abilities) {
            if (a.inRandomPool) l.Add(a);
        }
        List<RaceObject> races = new List<RaceObject>();
        foreach (RaceObject r in playerDataBase.races) {
            races.Add(r);
        }

        List<WeaponObject> weps = new List<WeaponObject>();
        foreach (WeaponObject r in playerDataBase.weapons) {
            weps.Add(r);
        }

        foreach (RunSelectFigure f in runFigures) {
            GenerateRunFigure(f, new List<RaceObject>(races), new List<AbilityObject>(l), new List<WeaponObject>(weps));
        }


        HideAll();
        runSelector.gameObject.SetActive(true);
    }


    public void ShowProfileSelectMenu() {
        HideAll();
        changeWindow.gameObject.SetActive(true);
        changeWindow.Setup();
    }

    public void ShowCreateProfileWindow(bool canCancel) {
        HideAll();
        createProfileWindow.gameObject.SetActive(true);
        createProfileWindow.Setup(canCancel);
    }

    public void Reroll() {
        string seed = DateTime.Now.GetHashCode().ToString();
        RoguelikeGameManager.currentSeed = seed;
        RoguelikeGameManager.currentProfile.currentSeed = seed;
        SaveUtility.SaveProfile(RoguelikeGameManager.currentProfile);
        ShowRunMenu();
    }

    public void GenerateRunFigure(RunSelectFigure figure, List<RaceObject> races, List<AbilityObject> abilities, List<WeaponObject> weapons) {

        RaceObject r = races.PickRandom();
        AbilityObject aOne = abilities.PickRandom();
        abilities.Remove(aOne);
        AbilityObject aTwo = abilities.PickRandom();
        WeaponObject w = weapons.PickRandom();


        figure.race = r;
        figure.weaponObject = w;
        figure.abilities = new List<AbilityObject>() { aOne, aTwo };
        figure.SetVisual();

    }

    public void StartRun() {
        player.DisablePlayerWithUI();
        player.transitioner.IntroTransition();
        RoguelikeGameManager.GoToMapLevel();
    }


}




