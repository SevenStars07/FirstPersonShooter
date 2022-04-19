using System;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerNameTracker : NetworkBehaviour
    {
        
        public static event Action<NetworkConnection, string> OnPlayerNameChanged;

        [SyncObject]
        private readonly SyncDictionary<NetworkConnection, string> _playerNames = new SyncDictionary<NetworkConnection, string>();
        
        private static PlayerNameTracker _instance;
        
        private void Awake()
        {
            _instance = this;
            _playerNames.OnChange += PlayerNames_OnChange;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            base.NetworkManager.ServerManager.OnRemoteConnectionState += ServerManager_OnRemoteConnectionState;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            base.NetworkManager.ServerManager.OnRemoteConnectionState -= ServerManager_OnRemoteConnectionState;
        }

        private void ServerManager_OnRemoteConnectionState(NetworkConnection arg1, RemoteConnectionStateArgs arg2)
        {
            if (arg2.ConnectionState == RemoteConnectionStates.Started)
                _playerNames.Remove(arg1);
        }

        private void PlayerNames_OnChange(SyncDictionaryOperation op, NetworkConnection key, string value, bool asserver)
        {
            if (op == SyncDictionaryOperation.Add || op == SyncDictionaryOperation.Set)
                OnPlayerNameChanged?.Invoke(key, value);
        }
        
        public static string GetPlayerName(NetworkConnection connection)
        {
            return _instance._playerNames.TryGetValue(connection, out string result) ? result : string.Empty;
        }

        [Client]
        public static void SetName(string name)
        {
            _instance.ServerSetName(name);
        }

        [ServerRpc(RequireOwnership = false)]
        private void ServerSetName(string s, NetworkConnection sender = null)
        {
            _playerNames[sender] = s;
        }
    }
}