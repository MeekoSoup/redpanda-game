using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialInputManager : MonoBehaviour
{
    public GameObject followCamera;
    public GameObject player;

    
    public Image    rightInputImage, 
                    leftInputImage, 
                    topInputImage, 
                    bottomInputImage, 
                    jumpInputImage, 
                        interactInputImage;

    public KeyCode rightInputKey = KeyCode.D;
    public KeyCode leftInputKey = KeyCode.A;
    public KeyCode topInputKey = KeyCode.W;
    public KeyCode bottomInputKey = KeyCode.S;
    public KeyCode jumpInputKey = KeyCode.Space;
    public KeyCode interactInputKey = KeyCode.E;

    public Color inactiveColor = Color.gray;
    public Color activeColor = Color.yellow;

    public Vector3 offset = Vector3.up * 0.1f;

    private void Start()
    {
        player = GameManager.instance.Player;
    }

    private void Update()
    {
        ChangeInputLook(rightInputKey, rightInputImage);
        ChangeInputLook(leftInputKey, leftInputImage);
        ChangeInputLook(topInputKey, topInputImage);
        ChangeInputLook(bottomInputKey, bottomInputImage);
        ChangeInputLook(jumpInputKey, jumpInputImage);
        ChangeInputLook(interactInputKey, interactInputImage);
        UpdatePosition();
    }

    private void ChangeInputLook(KeyCode key, Image image)
    {
        if (Input.GetKey(key))
        {
            image.color = activeColor;
        }
        else
        {
            image.color = inactiveColor;
        }
    }

    private void UpdatePosition()
    {
        transform.position = player.transform.position + offset;
        transform.rotation = followCamera.transform.rotation;
        transform.localEulerAngles = new Vector3(90f, transform.localEulerAngles.y, 0f);
    }
}
