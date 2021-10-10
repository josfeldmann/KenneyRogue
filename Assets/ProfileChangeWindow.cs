using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveProfile {
    public string profileName;
    public string currentSeed;
    public RunSaveData currentSaveData = null;

}

[System.Serializable]
public class RunSaveData {
    public string seed;
    public int levelIndex;
    public RogueHex[,] currentMap;
    public PlayerSaveData playerSaveData;

}

[System.Serializable]
public class PlayerSaveData {
    public int gold, xp;
    public string racekey;
    public string weaponkey;
    public string[] abilityKeys;
    public string[] itemKeys;
    public StatWrapper bonusStats;
}


public static class SaveUtility {
    public static List<SaveProfile> GetSaveProfiles() {
        List<SaveProfile> saves = new List<SaveProfile>();
        foreach (string s in Directory.GetDirectories(GetSaveDirectory())) {
            Debug.Log(s);
            if (File.Exists(s + "/savefile.json")) {
                saves.Add(JsonTool.StringToObject<SaveProfile>(File.ReadAllText(s + "/savefile.json")));
            }
        }
        return saves;
    }

    public static string GetSaveDirectory() {
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            return Application.dataPath + "/Saves/";
        } else {
            return Application.persistentDataPath + "/Saves/";
        }
    }

    public const string SavedProfileKey = "SavedProfile";

    public static SaveProfile GetLastSavedProfile() {
        if (PlayerPrefs.HasKey(SavedProfileKey)) {
            Debug.Log("HasKey");
            SaveProfile s = GetSaveProfile(PlayerPrefs.GetString(SavedProfileKey));
            if (s == null) {
                PlayerPrefs.DeleteKey(SavedProfileKey);
            }
            return s;
        }
        Debug.LogError("NOKEY");
        return null;
    }

    public static string GetSaveProfileLocation(string profileName) {
        return GetSaveDirectory() + profileName + "/";
    }

    public static string GetSaveProfileSaveFile(string profileName) {
        return GetSaveProfileLocation(profileName) + "savefile.json";
    }

    public static SaveProfile CreateNewProfile(string profileName) {
        if (!Directory.Exists(GetSaveProfileLocation(profileName))) {
            Directory.CreateDirectory(GetSaveProfileLocation(profileName));
        }
        SaveProfile s = new SaveProfile();
        s.profileName = profileName;
        s.currentSeed = System.DateTime.Now.GetHashCode().ToString();
        s.currentSaveData = null;

        File.WriteAllText(GetSaveProfileSaveFile(profileName), JsonTool.ObjectToString<SaveProfile>(s));
        SetLastProfile(profileName);
        return s;

    }

    public static void SetLastProfile(string profileName) {
        PlayerPrefs.SetString(SavedProfileKey, profileName);
    }

    public static SaveProfile GetSaveProfile(string profile) {
        if (File.Exists(GetSaveProfileSaveFile(profile))) {
            try {
                
                SaveProfile s = JsonTool.StringToObject<SaveProfile>(File.ReadAllText(GetSaveProfileSaveFile(profile)));
                return s;
            } catch {
                Debug.LogError("error reading profile: " + profile);
                return null;
            }

        } else {
            Debug.LogError("File doesn't exsit: " + GetSaveProfileSaveFile(profile));
        }
        return null;
    }


    public static void SaveProfile(SaveProfile s) {
        File.WriteAllText(GetSaveProfileSaveFile(s.profileName), JsonTool.ObjectToString<SaveProfile>(s));
    }

}

public class ProfileChangeWindow : MonoBehaviour {
    public Transform buttonGroupingTransform;
    public SelectProfileButton buttonPrefab;
    public List<SelectProfileButton> buttons = new List<SelectProfileButton>();
    private List<SaveProfile> profiles = new List<SaveProfile>();
    public Transform createButton;
    
    public void Setup() {
        if (!Directory.Exists(SaveUtility.GetSaveDirectory())) {
            Directory.CreateDirectory(SaveUtility.GetSaveDirectory());
        }
        if (buttons == null) buttons = new List<SelectProfileButton>();
        profiles = SaveUtility.GetSaveProfiles();

        foreach (SelectProfileButton p in buttons) {
            p.gameObject.SetActive(false);
        }
        for (int i = buttons.Count; i < profiles.Count; i++) {
            SelectProfileButton b = Instantiate(buttonPrefab, buttonGroupingTransform);
            buttons.Add(b);
        }

        for (int i = 0; i < profiles.Count; i++) {
            buttons[i].gameObject.SetActive(true);
            buttons[i].SetProfile(profiles[i]);
        }
        createButton.transform.SetAsLastSibling();
        
    }
}
