using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
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
        player = GameManager.instance.GetPlayer();
    }

    private void Update()
    {
        float dist = (player.transform.position - transform.position).sqrMagnitude;

        if (seeking)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
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
