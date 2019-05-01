using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerSpawnPoint;

    private GameObject player;
    public GameObject Player
    {
        get
        {
            if (player != null)
                return player;

            return player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void SpawnPlayerDefault()
    {
        if (player != null)
            return;

        if (playerPrefab == null)
        {
            Debug.LogError("Error: GameManager has no reference to playerPrefab.");
            return;
        }

        if (playerSpawnPoint == null)
        {
            Debug.LogError("Error: GameManager has no reference to playerSpawnPoint.");
            return;
        }

        player = Instantiate(playerPrefab,
            playerSpawnPoint.transform.position,
            playerSpawnPoint.transform.rotation);
        player.transform.SetParent(playerSpawnPoint.transform);
    }
}
