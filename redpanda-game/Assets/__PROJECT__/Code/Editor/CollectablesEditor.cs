using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Collectables))]
public class CollectablesEditor : Editor
{
    [SerializeField]
    private int coins, 
                copper, 
                silver, 
                gold, 
                copperWorth, 
                silverWorth, 
                goldWorth, 
                redpanda, 
                redpandaWorth;

    [SerializeField]
    private Collectable[] collectables;

    public override void OnInspectorGUI()
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

        base.OnInspectorGUI();

        var collectablesScript = target as Collectables;
        collectables = collectablesScript.gameObject.GetComponentsInChildren<Collectable>();

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

        int coinsWorth = copperWorth + silverWorth + goldWorth;
        int totalWorth = coinsWorth + redpandaWorth;

        GUILayout.Label(string.Format("Coins: {0} ({1}) {2:0.0}%", coins, coinsWorth, ((float)coinsWorth / totalWorth) * 100));
        GUILayout.Label(string.Format("Copper: {0} ({1}) {2:0.0}%", copper, copperWorth, ((float)copperWorth / totalWorth) * 100));
        GUILayout.Label(string.Format("Silver: {0} ({1}) {2:0.0}%", silver, silverWorth, ((float)silverWorth / totalWorth) * 100));
        GUILayout.Label(string.Format("Gold: {0} ({1}) {2:0.0}%", gold, goldWorth, ((float)goldWorth / totalWorth) * 100));
        GUILayout.Label(string.Format("RedPanda: {0} ({1}) {2:0.0}%", redpanda, redpandaWorth, ((float)redpandaWorth / totalWorth) * 100));
    }
}
