using UnityEngine;

public class HumanHelperAbility : Ability {

    public HumanHelper helperPrefab;

    public override void SkillDown() {
        if (!onCooldown) {
            SetOnCooldown();
            HumanHelper helper = Instantiate(helperPrefab, GetMouseClickPosition(), Quaternion.identity);
            
        }
    }
}
