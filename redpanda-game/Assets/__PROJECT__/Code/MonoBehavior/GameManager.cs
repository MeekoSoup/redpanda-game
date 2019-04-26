using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerSpawner playerSpawner;
    public PlayerMenu playerMenu;
    public GameObject mainCamera;
    [Space]
    public int score = 0;
    public bool paused = false;
    public bool lookingAtPlayerMenu = false;
    public float menuLerpTime = 1f;
    [Space]
    public TMP_Text scoreText;
    [Space]
    public KeyCode playerMenuKey = KeyCode.Escape;

    private vThirdPersonCamera vtpcScript;

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
        playerMenu = GameObject.FindGameObjectWithTag("PlayerMenu").GetComponent<PlayerMenu>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera == null)
        {
            Debug.LogError("Error: MainCamera not found.");
            return;
        }
        vtpcScript = mainCamera.GetComponent<vThirdPersonCamera>();

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

        if (Input.GetKeyDown(playerMenuKey))
        {
            paused = !paused;
            lookingAtPlayerMenu = !lookingAtPlayerMenu;
            vtpcScript.enabled = !vtpcScript.enabled;
            if (lookingAtPlayerMenu)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        if (!lookingAtPlayerMenu)
            return;

        if (mainCamera == null)
        {
            Debug.LogError("Error: Couldn't find MainCamera.");
            return;
        }

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, 
            playerMenu.cameraMount.transform.position, menuLerpTime * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, 
            playerMenu.cameraMount.transform.rotation, menuLerpTime * Time.deltaTime);
    }
}
