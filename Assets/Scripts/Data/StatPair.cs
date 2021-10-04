[System.Serializable]
public class StatPair {
    public StatEnum stat;
    public float statAmount = 0;

    public override string ToString() {
         return "+ " + statAmount + " " + StatEnumString(stat);
    }

    public static string StatEnumString(StatEnum s) {
        switch (s) {
            case StatEnum.MAXHP:
                return "MAX HP";
                break;
            case StatEnum.ATTACKDAMAGE:
                return "DAMAGE";
                break;
            case StatEnum.ATTACKSPEED:
                return "ATTACK SPEED";
                break;
            case StatEnum.MAGICDAMAGE:
                return "MAGIC DAMAGE";
                break;
            case StatEnum.MOVESPEED:
                return "MOVE SPEED";
                break;
            case StatEnum.COOLDOWN:
                return "COOLDOWN";
                break;
            case StatEnum.AGILITY:
                return "AGILITY";
                break;
            case StatEnum.STRENGTH:
                return "STRENGTH";
                break;
            case StatEnum.INTEL:
                return "INTELLIGENCE";
                break;
            case StatEnum.MANAREGEN:
                return "MANA REGEN";
                break;
            case StatEnum.MAXMANA:
                return "MAX MANA";
                break;
            case StatEnum.ARMOR:
                return "ARMOR";
                break;
        }
        return "MISSING";
    }

}


