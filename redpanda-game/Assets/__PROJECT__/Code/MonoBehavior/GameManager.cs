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
    [Space]
    public TMP_Text scoreText;
    [Space]
    public KeyCode playerMenuKey = KeyCode.Escape;
    //[HideInInspector]
    public float gameTimeScale = 1f; // used for most things outside menus and cameras
    public float metaTimeScale = 1f; // used for cameras and menus
    public float realTimeScale = 1f; // used for cameras and menus

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
        gameTimeScale = 1f;
        metaTimeScale = 1f;

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

        playerMenu.gameObject.SetActive(false);

        scoreText.text = score.ToString();
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
        Time.timeScale = realTimeScale;
        scoreText.text = score.ToString();

        // pause menu
        if (Input.GetKeyDown(playerMenuKey))
        {
            paused = !paused;
            lookingAtPlayerMenu = !lookingAtPlayerMenu;
            vtpcScript.enabled = !vtpcScript.enabled;
            if (lookingAtPlayerMenu)
            {
                Cursor.lockState = CursorLockMode.None;
                playerMenu.gameObject.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                playerMenu.gameObject.SetActive(false);
            }
        }

        if (!lookingAtPlayerMenu)
            return;

        if (mainCamera == null)
        {
            Debug.LogError("Error: Couldn't find MainCamera.");
            return;
        }

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, 
            playerMenu.cameraMount.transform.position, playerMenu.cameraLerpSpeed * MetaDeltaTime());
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, 
            playerMenu.cameraMount.transform.rotation, playerMenu.cameraLerpSpeed * MetaDeltaTime());
    }
}
