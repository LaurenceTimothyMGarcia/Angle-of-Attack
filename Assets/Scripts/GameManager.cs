using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Handles global activities within a single match, such
/// as respawning and pausing
/// </summary>

public class GameManager : NetworkBehaviour
{

    public static GameManager gm;

    public Transform playerPrefab;  //TODO: add prefabs for other characters
    public Transform spawnPoint;
    public int spawnDelay;

    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    //name will be used when multiple prefabs available
    public IEnumerator _RespawnPlayer(string name) {
        yield return new WaitForSeconds(spawnDelay);
        Transform clone = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void KillPlayer(GameObject player) {
        //string name = player.name;
        //NetworkManager.Destroy(player);
        //Transform clone = NetworkManager.Instantiate(gm.playerPrefab, gm.spawnPoint.position, gm.spawnPoint.rotation);
        //gm.StartCoroutine(gm._RespawnPlayer(name));
        //Still getting this to work :^((((((
    }
}
