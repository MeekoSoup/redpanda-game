using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Spinner : BaseBehavior
{
    [Range(0.1f, 50f)]
    public float spinFactor = 20f;
    public float spinSpeedMin = 1f;
    public float spinSpeedMax = 25f;
    public float maxDist = 50f;
    public float minDist = 3f;

    // Cached private properties
    private GameObject player = null;
    private float dist = 0;
    private float spinSpeed;

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
        //float distRatio = (dist - minDist) / (maxDist - minDist);
        float distRatio = (maxDist - minDist) / (dist - minDist);
        float speedDiff = spinSpeedMax - spinSpeedMin;

        if (dist > maxDist)
            spinSpeed = spinSpeedMin;
        else if (dist < minDist)
            spinSpeed = spinSpeedMax;
        else
            spinSpeed = ((distRatio * speedDiff) + spinSpeedMin) * spinFactor;

        // spinSpeed = Mathf.Clamp(spinFactor / (dist + 1), spinSpeedMin, spinSpeedMax) * time.deltaTime;

        transform.Rotate(Vector3.forward * spinSpeed * time.deltaTime);
    }
}
