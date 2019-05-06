using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CollectablesSummary : MonoBehaviour
{
    [HideInInspector]
    public int coins,
                copper,
                silver,
                gold,
                copperWorth,
                silverWorth,
                goldWorth,
                redpanda,
                redpandaWorth,
                coinsWorth,
                totalWorth;

    [SerializeField]
    private Collectable[] collectables;

    public void GenerateSummary()
    {
        coins = 0;
        copper = 0;
        silver = 0;
        gold = 0;
        copperWorth = 0;
        silverWorth = 0;
        goldWorth = 0;
        redpanda = 0;
        redpandaWorth = 0;
        coinsWorth = 0;
        totalWorth = 0;
        
        collectables = gameObject.GetComponentsInChildren<Collectable>();

        foreach (Collectable collectable in collectables)
        {
            if (collectable.collectableType == Collectable.CollectableType.Coin)
                coins++;
            if (collectable.collectableType == Collectable.CollectableType.RedPanda)
                redpanda++;
            if (collectable.quality == Collectable.Quality.Copper)
            {
                copper++;
                copperWorth += Collectable.COPPER_WORTH;
            }
            if (collectable.quality == Collectable.Quality.Silver)
            {
                silver++;
                silverWorth += Collectable.SILVER_WORTH;
            }
            if (collectable.quality == Collectable.Quality.Gold)
            {
                gold++;
                goldWorth += Collectable.GOLD_WORTH;
            }
            if (collectable.collectableType == Collectable.CollectableType.RedPanda)
            {
                redpanda++;
                redpandaWorth += Collectable.REDPANDA_WORTH;
            }
        }

        coinsWorth = copperWorth + silverWorth + goldWorth;
        totalWorth = coinsWorth + redpandaWorth;
    }


}
