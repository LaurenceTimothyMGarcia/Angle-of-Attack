using Singleton;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame {
        get {
            return playersInGame.Value;
        }
    }

    // private void Start() {

    //     // On player joins, increase playersIngame by 1
    //     NetworkManager.Singleton.OnClientConnectedCallback += (id) => {
    //         if(IsServer) {
    //             Debug.Log($"Player {id} joined. Player count: " + playersInGame.Value);
    //             playersInGame.Value++;
    //         }
    //     };

    //     // On player leave, decrease playersIngame by 1
    //     NetworkManager.Singleton.OnClientDisconnectCallback += (id) => {
    //         if(IsServer) {
    //             Debug.Log($"Player {id} left. Player count: " + playersInGame.Value);
    //             playersInGame.Value--;
    //         }
    //     };
    // }
}
