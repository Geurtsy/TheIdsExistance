using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempModeSkipper : MonoBehaviour {

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            GameObject.FindGameObjectWithTag("GameGod").GetComponent<GameGod>().EndMode(true);
        
        if(Input.GetKeyDown(KeyCode.Return))
            GameObject.FindGameObjectWithTag("GameGod").GetComponent<GameGod>().EndMode(false);
    }
}
