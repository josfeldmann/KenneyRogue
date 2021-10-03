[System.Serializable]
public class StatWrapper {
    public float maxHP = 0;
    public float maxMana = 0;
    public float agility = 0;
    public float intelligence = 0;
    public float strength = 0;
    public float manaRegen = 0;
    public float armor = 0;
    public float attackDamage = 0;
    public float spellAmp = 0;
    public float attackSpeed = 0;
    public float magicAmp = 0;
    public float moveSpeed = 0;
    public float coolDownReduction = 0;

    public StatWrapper() {

    }

    public StatWrapper(float maxHP, float maxMana, float agility, float intelligence, float strength, float manaRegen, float armor, float attackDamage, float spellAmp, float attackSpeed, float magicAmp, float moveSpeed, float coolDownReduction) {
        this.maxHP = maxHP;
        this.maxMana = maxMana;
        this.agility = agility;
        this.intelligence = intelligence;
        this.strength = strength;
        this.manaRegen = manaRegen;
        this.armor = armor;
        this.attackDamage = attackDamage;
        this.spellAmp = spellAmp;
        this.attackSpeed = attackSpeed;
        this.magicAmp = magicAmp;
        this.moveSpeed = moveSpeed;
        this.coolDownReduction = coolDownReduction;
    }

    public static StatWrapper operator +(StatWrapper s1, StatWrapper s2) => new StatWrapper(s1.maxHP + s2.maxHP, s1.maxMana + s2.maxMana, s1.agility + s2.agility, s1.intelligence + s2.intelligence, s1.strength + s2.strength,
                                                                                            s1.manaRegen + s2.manaRegen, s1.armor + s2.armor, s1.attackDamage + s2.attackDamage, s1.spellAmp + s2.spellAmp, s1.attackSpeed + s2.attackSpeed,
                                                                                            s1.magicAmp + s2.magicAmp, s1.moveSpeed + s2.moveSpeed, s1.coolDownReduction + s2.coolDownReduction);

}


