using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fidgetter : MonoBehaviour
{
    public string[] animations;
    public float fidgetTime = 5f;

    private float fidgetTimeElapsed;
    private Animator anim;

    private void Awake()
    {
        fidgetTimeElapsed = fidgetTime;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (fidgetTimeElapsed <= 0)
        {
            fidgetTimeElapsed = fidgetTime;
            int r = Random.Range(0, animations.Length);
            anim.Play(animations[r], 0, 0);
        }
        fidgetTimeElapsed -= GameManager.instance.MetaDeltaTime();
    }
}
