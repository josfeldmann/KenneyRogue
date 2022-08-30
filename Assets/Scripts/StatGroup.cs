using UnityEngine;
using UnityEngine.Localization;
[System.Serializable]
public class StatGroup {
    public StatEnum statEnum;
    public LocalizedString statName;
    public LocalizedString description;
    public Sprite sprite;

    public string GetName() {
        return statName.GetLocalizedString();
    }

    public string GetDescription() {
        return description.GetLocalizedString();
    }
}
