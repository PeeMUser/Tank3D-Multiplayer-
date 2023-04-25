using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerName : NetworkBehaviour
{
    private TextMesh playerName;
    [Networked]
    public string playerID { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        playerName = GetComponent<TextMesh>();
        if (Object.HasInputAuthority)
        {

            playerName = GetComponent<TextMesh>();
            playerID = Object.Id.ToString();
            playerName.text = playerID;
        }



    }
    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.All)]
    public void RPC_Config(string name)
    {
        playerName.text = name;
        Object.SendMessage(playerID);

    }
}
