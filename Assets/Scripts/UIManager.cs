using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text allyInformation;
    public TMP_Text enemyInformation;

    void Start()
    {
        CombatManager.Instance.RoundOverEvent += OnRoundOver;
        RefreshInformation();
    }

    private void OnRoundOver(object sender, EventArgs e) {
        RefreshInformation();
    }
    
    private void RefreshInformation() {
        if (allyInformation != null) {
            allyInformation.text = "";
            foreach (Ally ally in CombatManager.Instance.GetAllies()) {
                allyInformation.text += ally + "\n";
            }
        }
        if (enemyInformation != null) {
            enemyInformation.text = "";
            foreach (Enemy enemy in CombatManager.Instance.GetEnemies()) {
                enemyInformation.text += enemy + "\n";
            }
        }
    }
}
