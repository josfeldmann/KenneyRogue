using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BattleMapLoader : MonoBehaviour
{

    public AssetReference mapRef;
    public BattleMap battleMap;
    public void Awake() {
        foreach (RoguelikePlayer player in RoguelikePlayer.players) {
            player.Reenable();
        }
        StartCoroutine(SpawnMap());
    }

    public void GoToWorldMap() {

    }


    public IEnumerator SpawnMap() {


        yield return null;

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(mapRef);
        yield return handle;
        if (handle.Result != null) {
          battleMap = Instantiate(handle.Result).GetComponent<BattleMap>();
            battleMap.SpawnEnemies();
        }
    }



}


