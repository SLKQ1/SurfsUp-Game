using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : Mirror.NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("SetName", 0f, 1f); 
    }

    void SetName()
	{
		if (isLocalPlayer)
		{
			GetComponent<TextMesh>().text = PlayerInfo.PlayerNameStatic;
		}
	}
}
