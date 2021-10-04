using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : Singleton<TextSpawner>
{
    public ObjectPooler textPool;



    public static void SpawnTextAt(Vector3 pos, string text, float lingerTime) {
        DamageText t = (DamageText)(Instance.textPool.CreateObjectAt(pos, Quaternion.identity));
        t.Set(pos, text, lingerTime);
    }

}
