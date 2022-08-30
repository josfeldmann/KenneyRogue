using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : Ability
{
 

        public MeleeEnemy bombPrefab;

        public override void SkillDown() {
            if (!onCooldown) {
                SetOnCooldown();

                List<Vector3> v = GetSpotsAroundPoint( player.transform.position, 3, Random.Range(0, 360f), 1);
                

                foreach (Vector3 vv in  v) {
                    MeleeEnemy m = Instantiate(bombPrefab, vv, Quaternion.identity);
                }

            }
        }
    
}
