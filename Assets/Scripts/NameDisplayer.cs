using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using FishNet.Connection;
using FishNet.Object;
using TMPro;
using UnityEngine;

public class NameDisplayer : NetworkBehaviour
{
    [SerializeField] private TextMeshPro _text;
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        SetName();
        
        PlayerNameTracker.OnPlayerNameChanged += PlayerNameTracker_OnPlayerNameChanged;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        PlayerNameTracker.OnPlayerNameChanged -= PlayerNameTracker_OnPlayerNameChanged;

    }

    private void PlayerNameTracker_OnPlayerNameChanged(NetworkConnection arg1, string arg2)
    {
        if (arg1 != Owner) return;
        
        SetName();
    }

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
        base.OnOwnershipClient(prevOwner);
        SetName();
    }

    private void SetName()
    {
        string result = null;

        if (Owner.IsValid)
            result = PlayerNameTracker.GetPlayerName(Owner);
        
        if (string.IsNullOrEmpty(result))
            result = OwnerId.ToString();

        _text.text = result;
    }
}
