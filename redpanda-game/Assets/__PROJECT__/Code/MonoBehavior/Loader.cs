using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.instance != null)
            return;

        Instantiate(gameManager);
    }

    public void LoadLevel(string scene)
    {
        GameManager.instance.LoadScene(scene);
    }
}
