using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
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
            myTransform.GetChild(1).GetComponent<TextMeshPro>().text = playerUniqueIdentity;
        } else
        {
            myTransform.GetChild(0).GetComponent<TextMeshPro>().text = MakeUniqueIdentity();
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
