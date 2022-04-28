using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Handles global activities within a single match, such
/// as respawning and pausing
/// </summary>

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public Transform playerPrefab;  //TODO: add prefabs for other characters
    public Transform spawnPoint;
    public int spawnDelay;

    void Start()
    {
        if (gm == null) {
            gm = GameObject.Find("GM").GetComponent<GameManager>();
        }
    }


    void Update()
    {
        
    }

    //name will be used when there are multiple prefabs/characters to choose from
    private IEnumerator _RespawnHelper(string name) {
        yield return new WaitForSeconds(spawnDelay);
        Transform clone = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void RespawnPlayer(GameObject player) {
        string name = player.name;
        Destroy(player);
        gm.StartCoroutine(gm._RespawnHelper(name));
        
    }
}
