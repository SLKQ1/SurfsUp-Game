using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : Mirror.NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     if(isLocalPlayer) {
            GetComponent<TextMesh>().text = PlayerInfo.PlayerNameStatic; 
        }
    }
}
