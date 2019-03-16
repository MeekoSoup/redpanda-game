using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerSpawner playerSpawner;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        InitGame();
    }

    public void InitGame()
    {
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner").GetComponent<PlayerSpawner>();
    }

    public GameObject GetPlayer()
    {
        return playerSpawner.Player;
    }
}
