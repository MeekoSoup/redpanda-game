using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    // GUI
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.scoreText = scoreText;
            return;
        }

        Instantiate(gameManager);
        GameManager.instance.scoreText = scoreText;
    }
}
