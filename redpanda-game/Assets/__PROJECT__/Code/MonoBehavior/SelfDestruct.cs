using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip("Time it takes before objects destroys itself.")]
    public float destroyTime = 1f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
