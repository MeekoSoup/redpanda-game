using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMenu : MonoBehaviour
{
    public Color checkedColor = Color.green;
    public Color uncheckedColor = Color.gray;
    [Space]
    public Image resumeImage;
    public Image optionsImage;
    public Image hintsImage;
    public Image soundImage;
    public Image musicImage;
    public Image mainMenuImage;
    public Image exitImage;
    [Space]
    public GameObject cameraMount;
    public float cameraLerpSpeed = 3f;
}
