using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CollectablesSummary))]
public class CollectablesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var summary = target as CollectablesSummary;

        summary.GenerateSummary();

        base.OnInspectorGUI();

        GUILayout.Label(string.Format("Coins: {0} ({1}) {2:0.0}%", 
            summary.coins, summary.coinsWorth, ((float)summary.coinsWorth / summary.totalWorth) * 100));
        GUILayout.Label(string.Format("Copper: {0} ({1}) {2:0.0}%", 
            summary.copper, summary.copperWorth, ((float)summary.copperWorth / summary.totalWorth) * 100));
        GUILayout.Label(string.Format("Silver: {0} ({1}) {2:0.0}%", 
            summary.silver, summary.silverWorth, ((float)summary.silverWorth / summary.totalWorth) * 100));
        GUILayout.Label(string.Format("Gold: {0} ({1}) {2:0.0}%", 
            summary.gold, summary.goldWorth, ((float)summary.goldWorth / summary.totalWorth) * 100));
        GUILayout.Label(string.Format("RedPanda: {0} ({1}) {2:0.0}%",
            summary.redpanda, summary.redpandaWorth, ((float)summary.redpandaWorth / summary.totalWorth) * 100));
        GUILayout.Label(string.Format("Total Worth: {0}", summary.totalWorth));
    }
}
