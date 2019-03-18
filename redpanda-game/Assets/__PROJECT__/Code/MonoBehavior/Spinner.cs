using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float spinSpeed = 20f;
    public float spinSpeedMin = 1f;
    public float spinSpeedMax = 25f;

    // Cached private properties
    private GameObject player = null;
    private float dist = 0;
    private float spinFactor = 0;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Debug.Log("GameManager.instance == null");
            return;
        }

        if (GameManager.instance.GetPlayer() == null)
        {
            Debug.Log("GameManager.instance.GetPlayer() == null");
            return;
        }

        player = GameManager.instance.GetPlayer();
    }

    private void Update()
    {
        if (player == null)
            return;

        dist = (transform.position - player.transform.position).sqrMagnitude;
        spinFactor = Mathf.Clamp(spinSpeed / (dist + 1) * spinSpeed * Time.deltaTime, spinSpeedMin, spinSpeedMax);
        transform.Rotate(Vector3.forward * spinFactor);
    }
}
