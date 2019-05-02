﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : BaseBehavior
{
    public int worth = 5;
    [Space]
    public float seekDist = 5f;
    public float collectDist = 0.5f;
    public float speed = 1f;
    [Space]
    public GameObject coinShatter;

    private bool seeking = false;
    private GameObject player;

    private void Awake()
    {
        player = GameManager.instance.Player;
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogError("Error: Player not found.");
            return;
        }

        float dist = (player.transform.position - transform.position).sqrMagnitude;

        if (seeking)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * time.deltaTime);
            if (dist <= collectDist)
                Collect();

            return;
        }

        if (dist <= seekDist)
            seeking = true;
    }

    public void Collect()
    {
        Instantiate(coinShatter, transform.position, transform.rotation);
        GameManager.instance.score += worth;
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
