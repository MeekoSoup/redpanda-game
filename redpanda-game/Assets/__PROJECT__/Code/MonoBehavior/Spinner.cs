using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float spinSpeed = 20f;
    public float spinDist = 50f;

    private SceneManager sm;

    [ShowInInspector]
    private float dist;
    [ShowInInspector]
    private float math;

    private void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
    }

    private void Update()
    {
        Vector3 offset = transform.position - sm.player.transform.position;
        dist = offset.sqrMagnitude;
        dist = Vector3.Distance(transform.position, sm.player.transform.position);
        math = (100 / (dist + 0.1f)) * spinSpeed;
        if (dist < spinDist)
        {
            transform.Rotate(Vector3.forward * math * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        }
    }
}
