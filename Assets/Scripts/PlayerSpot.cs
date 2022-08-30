using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpot : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        RoguelikePlayer player = RoguelikeGameManager.player;
        player.Reenable();
        player.transform.position = transform.position;
    }
}
    
