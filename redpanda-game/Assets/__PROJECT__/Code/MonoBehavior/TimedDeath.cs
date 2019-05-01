using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : BaseBehavior
{
    public float timeUntilDeath = 5f;

    private void Start()
    {
        Destroy(gameObject, timeUntilDeath);
    }
}
