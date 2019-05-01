using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class PlayerMenu : MonoBehaviour
{
    public Color checkedColor = Color.green;
    public Color uncheckedColor = Color.gray;
    [Space]
    public GameObject resumeButton;
    public GameObject optionsButton;
    public GameObject hintsButton;
    public GameObject soundButton;
    public GameObject musicButton;
    public GameObject mainMenuButton;
    public GameObject exitButton;
    [Space]
    public TMP_Text hintsText;
    public TMP_Text soundText;
    public TMP_Text musicText;
    [Space]
    public GameObject cameraMount;
    public float cameraLerpSpeed = 3f;
    [Space]
    public Color toggleOnColor = Color.gray;
    public Color toggleOffColor = Color.white;

    [Button, ButtonGroup("Toggles")]
    public void SoundOn()
    {
        Image image = soundButton.GetComponent<Image>();
        image.color = toggleOnColor;
        soundText.text = "Sound On";
    }

    [Button, ButtonGroup("Toggles")]
    public void SoundOff()
    {
        Image image = soundButton.GetComponent<Image>();
        image.color = toggleOffColor;
        soundText.text = "Sound Off";
    }

    [Button, ButtonGroup("Toggles")]
    public void MusicOn()
    {
        Image image = musicButton.GetComponent<Image>();
        image.color = toggleOnColor;
        musicText.text = "Music On";
    }

    [Button, ButtonGroup("Toggles")]
    public void MusicOff()
    {
        Image image = musicButton.GetComponent<Image>();
        image.color = toggleOffColor;
        musicText.text = "Music Off";
    }

    [Button, ButtonGroup("Toggles")]
    public void HintsOn()
    {
        Image image = hintsButton.GetComponent<Image>();
        image.color = toggleOnColor;
        hintsText.text = "Hints On";
    }

    [Button, ButtonGroup("Toggles")]
    public void HintsOff()
    {
        Image image = hintsButton.GetComponent<Image>();
        image.color = toggleOffColor;
        hintsText.text = "Hints Off";
    }
}
