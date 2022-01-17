using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Enemies;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    
    void Start()
    {
        CombatManager.Instance.RoundOverEvent += OnRoundOver;
        RefreshInformation();
    }

    private void OnRoundOver(object sender, EventArgs e) {
        RefreshInformation();
    }
    
    private void RefreshInformation() {
        foreach (Ally ally in CombatManager.Instance.GetAllies()) {
            StatDisplay statDisplay = ally.gameObject.GetComponentInChildren<StatDisplay>();
            statDisplay.DisplayStatusValues(ally);
        }
        
        foreach (Enemy enemy in CombatManager.Instance.GetEnemies()) {
            StatDisplay statDisplay = enemy.gameObject.GetComponentInChildren<StatDisplay>();
            statDisplay.DisplayStatusValues(enemy);
        }
    }
}
