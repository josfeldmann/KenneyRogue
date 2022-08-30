using UnityEngine;

public class HumanHelperAbility : Ability {

    public MeleeEnemy bombPrefab;

    public override void SkillDown() {
        if (!onCooldown) {
            SetOnCooldown();
            MeleeEnemy helper = Instantiate(bombPrefab, GetMouseClickPosition(), Quaternion.identity);
            
        }
    }
}
