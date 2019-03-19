using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerSpawner playerSpawner;
    [Space]
    public int score = 0;
    [Space]
    public TMP_Text scoreText;

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

        if (scoreText == null)
            return;

        scoreText.text = score.ToString();
    }

    public GameObject GetPlayer()
    {
        return playerSpawner.Player;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }
}
