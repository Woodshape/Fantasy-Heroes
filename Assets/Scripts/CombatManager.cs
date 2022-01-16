using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public static CombatManager Instance;

    public GameObject[] startingAllies;
    public GameObject[] startingEnemies;
    
    private Dictionary<int, Ally> allies = new Dictionary<int, Ally>();
    private Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();
    
    [SerializeField]
    private int positions = 6;
    private int positionCounter = 0;
    
    [SerializeField]
    private float turnTime = 1;
    private float turnTimer;
    private float turnNumber;

    [SerializeField]
    private bool autoTurn;

    private bool combatOver;
    
    public event EventHandler RoundOver;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        
        for (int i = 0; i < startingAllies.Length; i++) {
            int pos = i + 1;
            if (pos <= positions) {
                Ally ally = startingAllies[i].GetComponent<Ally>();
                if (ally != null) {
                    Debug.Log($"Adding ally at position {pos}: {ally}");
                    allies.Add(pos, ally);
                }
                else {
                    Debug.LogWarning("No ally on GameObject: " + startingAllies[i]);
                }
                
                Enemy enemy = startingEnemies[i].GetComponent<Enemy>();
                if (enemy != null) {
                    Debug.Log($"Adding ally at position {pos}: {enemy}");
                    enemies.Add(pos, enemy);
                }
                else {
                    Debug.LogWarning("No enemy on GameObject: " + startingEnemies[i]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (combatOver) {
            return;
        }
        
        if (autoTurn) {
            turnTimer += Time.deltaTime;
            if (turnTimer >= turnTime) {
                turnTimer = 0f;

                NextTurn();
            }
        }
    }
    
    public Ally GetFrontAlly() {
        var sorted = allies.OrderBy(kvp => kvp.Key).ToList();
        return sorted[0].Value;
    }
    
    public Enemy GetFrontEnemy() {
        var sorted = enemies.OrderBy(kvp => kvp.Key).ToList();
        return sorted[0].Value;
    }
    
    private void NextTurn() {
        turnNumber++;
        positionCounter++;
        if (positionCounter > positions) {
            positionCounter = 1;

            Debug.Log("Round finished...");
        }
        
        Debug.Log($"Turn: {turnNumber} | Position: {positionCounter}");
        // Character ally = GetActiveAlly(positionCounter);
        // Character enemy = GetActiveEnemy(positionCounter);
        Ally ally = GetFrontAlly();
        Enemy enemy = GetFrontEnemy();

        if (ally != null && enemy != null) {
            DetermineTurnOrder(ally, enemy);
        }
    }

    private void DetermineTurnOrder(Ally ally, Enemy enemy) {
        int allySpeed = ally.Speed;
        int enemySpeed = enemy.Speed;
        
        Debug.Log($"Ally speed: {allySpeed} | Enemy speed: {enemySpeed}");
        
        //  Ally / enemy turn order is based on speed
        if (allySpeed > enemySpeed) {
            ally.TakeTurn(positionCounter);
            
            EvaluateRound();
            
            if (!enemy.IsDead()) {
                enemy.TakeTurn(positionCounter);
            }
        }
        else if (allySpeed == enemySpeed){
            //  Take turns simultaneous
            ally.TakeTurn(positionCounter);
            enemy.TakeTurn(positionCounter);
            
            EvaluateRound();
        }
        else {
            enemy.TakeTurn(positionCounter);
            
            EvaluateRound();
            
            if (!ally.IsDead()) {
                ally.TakeTurn(positionCounter);
            }
        }
        
        EvaluateTurn();
    }

    private void EvaluateRound() {
        foreach (var ally in allies) {
            Ally al = ally.Value;
            al.EvaluateHealth();
        }
        foreach (var enemy in enemies) {
            Enemy en = enemy.Value;
            en.EvaluateHealth();
        }

        OnRoundOver();
    }

    private void EvaluateTurn() {
        List<int> alliesToRemove = new List<int>();
        foreach (var ally in allies) {
            Ally al = ally.Value;
            if (al.IsDead()) {
                alliesToRemove.Add(ally.Key);
                al.Die();
            }
        }

        foreach (int index in alliesToRemove) {
            allies.Remove(index);
        }

        List<int> enemiesToRemove = new List<int>();
        foreach (var enemy in enemies) {
            Enemy en = enemy.Value;
            if (en.IsDead()) {
                enemiesToRemove.Add(enemy.Key);
                en.Die();
            }
        }
        
        foreach (int index in enemiesToRemove) {
            enemies.Remove(index);
        }
        
        OnRoundOver();

        DetermineWinner();
    }
    
    private void DetermineWinner() {
        if (allies.Count == 0 && enemies.Count == 0){
            Debug.Log("COMBAT OVER: DRAW!");
            combatOver = true;
        }
        else if (allies.Count == 0) {
            Debug.Log("COMBAT OVER: ENEMIES WON!");
            combatOver = true;
        }
        else if (enemies.Count == 0) {
            Debug.Log("COMBAT OVER: ALLIES WON!");
            combatOver = true;
        }
        else {
            Debug.Log("No winner: New Round...");
        }
    }

    private Ally GetActiveAlly(int position) {
        bool allyForPosition = allies.TryGetValue(position, out Ally character);
        if (allyForPosition) {
            Debug.Log("Active Ally: " + character);
            return character;
        }

        Debug.LogWarning($"No ally for position {position}: {allies}");
        
        return null;
    }
    
    private Enemy GetActiveEnemy(int position) {
        bool enemyForPosition = enemies.TryGetValue(position, out Enemy character);
        if (enemyForPosition) {
            Debug.Log("Active Enemy: " + character);
            return character;
        }

        Debug.LogWarning($"No enemy for position {position}: {enemies}");
        
        return null;
    }
    
    protected virtual void OnRoundOver() {
        RoundOver?.Invoke(this, EventArgs.Empty);
    }

    public List<Ally> GetAllies() {
        return new List<Ally>(allies.Values);
    }
    
    public List<Enemy> GetEnemies() {
        return new List<Enemy>(enemies.Values);
    }
}
