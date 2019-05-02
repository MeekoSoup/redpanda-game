﻿using UnityEngine;
using TMPro;
using Chronos;
using UnityEngine.SceneManagement;

public class GameManager : BaseBehavior
{
    public static GameManager instance = null;
    public PlayerSpawner playerSpawner;
    public PlayerMenu playerMenu;
    public GameObject mainCamera;
    public HUD hud;
    [Space]
    public int score = 0;
    public bool paused = false;
    public bool lookingAtPlayerMenu = false;
    public bool hints = true;
    [Space]
    public KeyCode playerMenuKey = KeyCode.Escape;
    //[HideInInspector]
    public float gameTimeScale = 1f; // used for most things outside menus and cameras
    public float metaTimeScale = 1f; // used for cameras and menus
    public float realTimeScale = 1f; // used for cameras and menus

    private vThirdPersonCamera vtpcScript;

    private Clock rootClock;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //rootClock = Timekeeper.instance.Clock("root");

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    public void InitGame()
    {
        gameTimeScale = 1f;
        metaTimeScale = 1f;

        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner").GetComponent<PlayerSpawner>();
        playerMenu = GameObject.FindGameObjectWithTag("PlayerMenu").GetComponent<PlayerMenu>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        if (mainCamera == null)
        {
            Debug.LogError("Error: MainCamera not found.");
            return;
        }

        if (hud == null)
        {
            Debug.LogError("Error: HUD not found.");
            return;
        }
        
        if (playerMenu == null)
        {
            Debug.LogError("Error: PlayerMenu not found.");
            return;
        }

        if (playerSpawner == null)
        {
            Debug.LogError("Error: PlayerSpawner not found.");
            return;
        }
        vtpcScript = mainCamera.GetComponent<vThirdPersonCamera>();

        playerMenu.gameObject.SetActive(false);
        hints = true;

        UnpauseGame();

        HintsOn();

        hud.scoreText.text = score.ToString();
    }

    #region Time Functions

    public float GameDeltaTime()
    {
        return Time.deltaTime * Mathf.Max(gameTimeScale, 0f);
    }

    public float MetaDeltaTime()
    {
        return Time.deltaTime * Mathf.Max(metaTimeScale, 0f);
    }

    public float FixedGameDeltaTime()
    {
        return Time.fixedDeltaTime * Mathf.Max(gameTimeScale, 0f);
    }

    public float FixedMetaDeltaTime()
    {
        return Time.fixedDeltaTime * Mathf.Max(metaTimeScale, 0f);
    }

    #endregion

    public GameObject GetPlayer()
    {
        return playerSpawner.Player;
    }

    private void Update()
    {
        //  Time.timeScale = realTimeScale;
        hud.scoreText.text = score.ToString();

        // pause menu
        if (Input.GetKeyDown(playerMenuKey))
        {
            TogglePauseGame();
        }

        PausedGameEffects();
    }

    public void TogglePauseGame()
    {       
        vtpcScript.enabled = !vtpcScript.enabled;
        if (paused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = true;
        vtpcScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        playerMenu.gameObject.SetActive(true);  
    }

    public void UnpauseGame()
    {
        paused = false;
        vtpcScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        playerMenu.gameObject.SetActive(false);
    }

    public void PausedGameEffects() 
    {
        if (!paused)
            return;

        if (mainCamera == null)
        {
            Debug.LogError("Error: Couldn't find MainCamera.");
            return;
        }

        if (playerMenu == null)
        {
            Debug.LogError("Error: Couldn't find playerMenu.");
            return;
        }

        if (playerMenu.cameraMount == null)
        {
            Debug.LogError("Error: Couldn't find playerMenu.cameraMount .");
            return;
        }

        if (time == null)
        {
            Debug.LogError("Error: Couldn't find time   .");
            return;
        }

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, 
            playerMenu.cameraMount.transform.position, playerMenu.cameraLerpSpeed * time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation,
            playerMenu.cameraMount.transform.rotation, playerMenu.cameraLerpSpeed * time.deltaTime);
    }

    public void ToggleHints()
    {
        if (hints)
            HintsOff();
        else
            HintsOn();
    }

    public void HintsOn()
    {
        hints = true;
        playerMenu.HintsOn();
        playerMenu.hintsObject.SetActive(true);
    }

    public void HintsOff()
    {
        hints = false;
        playerMenu.HintsOff();
        playerMenu.hintsObject.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Quiting game!");
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
