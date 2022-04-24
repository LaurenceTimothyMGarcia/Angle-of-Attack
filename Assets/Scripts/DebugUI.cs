using Unity.Netcode;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    void OnGUI() {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();

            // DirectionalMovement(new Vector3(0, 0, 0));
        }

        GUILayout.EndArea();
    }

    static void StartButtons() {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels() {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label(
            "Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name
        );
        GUILayout.Label("Mode: " + mode);
    }

    // static void DirectionalMovement(Vector3 direction, ForceMode mode = ForceMode.Force) {
    //     if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change")) {
    //         if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient ) {
    //             foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
    //                 NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<base_movement.TestPlayer>().PlayerForce(direction, mode);
    //         } else {
    //             var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
    //             var player = playerObject.GetComponent<base_movement.TestPlayer>();
    //             player.PlayerForce(direction, mode);
    //         }
    //     }
    // }
}
