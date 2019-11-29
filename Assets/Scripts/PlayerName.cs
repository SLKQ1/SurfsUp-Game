using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerName : NetworkBehaviour
{
    [SyncVar] public string playerUniqueIdentity;
    private Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }

    void Awake()
    {
        myTransform = transform;
    }

    void Update()
    {
        if(myTransform.name == "" || myTransform.name == "Player(Clone)")
        {
            SetIdentity();
        } 
    }



    [Client]
    void GetNetIdentity()
    {
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    [Client]
    void SetIdentity()
    {
        if(!isLocalPlayer)
        {
            myTransform.GetChild(1).GetComponent<TextMesh>().text = playerUniqueIdentity;
        } else
        {
            myTransform.GetChild(0).GetComponent<TextMesh>().text = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        return PlayerInfo.PlayerNameStatic;
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueIdentity = name;
    }
}
