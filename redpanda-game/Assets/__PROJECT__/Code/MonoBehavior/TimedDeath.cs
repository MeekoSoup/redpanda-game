using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    public float time = 5f;

    private void Start()
    {
        Destroy(gameObject, time);
    }
}
