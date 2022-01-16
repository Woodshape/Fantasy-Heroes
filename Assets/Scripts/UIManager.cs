using System;
using System.Collections;
using System.Collections.Generic;
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
            TMP_Text text = ally.gameObject.GetComponentInChildren<TMP_Text>();
            text.text = GetStatusValues(ally);
        }
        
        foreach (Enemy enemy in CombatManager.Instance.GetEnemies()) {
            TMP_Text text = enemy.gameObject.GetComponentInChildren<TMP_Text>();
            text.text = GetStatusValues(enemy);
        }
    }

    private string GetStatusValues(Character character) {
        return $"{character.Power}|{character.Speed}|{character.Health}";
    }
}
