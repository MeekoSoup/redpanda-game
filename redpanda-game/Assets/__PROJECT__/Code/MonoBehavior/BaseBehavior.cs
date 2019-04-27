using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class BaseBehavior : MonoBehaviour
{
    public Timeline time
    {
        get
        {
            return GetComponent<Timeline>();
        }
    }
}
