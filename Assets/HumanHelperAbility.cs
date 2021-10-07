using UnityEngine;

public class HumanHelperAbility : Ability {

    public MeleeEnemy helperPrefab;

    public override void SkillDown() {
        if (!onCooldown) {
            SetOnCooldown();
            MeleeEnemy helper = Instantiate(helperPrefab, GetMouseClickPosition(), Quaternion.identity);
            
        }
    }
}
