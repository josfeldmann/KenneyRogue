using UnityEngine;

public delegate void VoidDelegate();
public delegate void DamageDelegate(float amt);

public class Unit : MonoBehaviour {

    [HideInInspector]public bool isDead = false;
    public DamageDelegate onTakeDamage;
    public VoidDelegate onDeath;
    public Rigidbody2D rb;
    public float maxHp = 5, currentHP = 5;
    public bool canTakeDamage = true;
    public void TakeDamage(float amt) {

        if (canTakeDamage && !isDead) {
            currentHP -= amt;
            
            if (currentHP <= 0) {
                currentHP = 0;
                isDead = true;
                if (onDeath != null) onDeath.Invoke();
            }
            if (onTakeDamage != null) onTakeDamage.Invoke(amt);
        }


    }

}
