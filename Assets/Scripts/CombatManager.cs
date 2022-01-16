using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public static CombatManager Instance;
    
    public GameObject[] startingAllies;
    public GameObject[] startingEnemies;
    
    private Dictionary<int, Character> allies = new Dictionary<int, Character>();
    private Dictionary<int, Character> enemies = new Dictionary<int, Character>();
    
    [SerializeField]
    private int positions = 6;
    private int positionCounter = 0;
    
    [SerializeField]
    private float turnTime = 1;
    private float turnTimer;
    private float turnNumber;

    [SerializeField]
    private bool autoTurn;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    
    void Start()
    {
        for (int i = 0; i < startingAllies.Length; i++) {
            int pos = i + 1;
            if (pos <= positions) {
                Character ally = startingAllies[i].GetComponent<Character>();
                if (ally != null) {
                    Debug.Log($"Adding ally at position {pos}: {ally}");
                    allies.Add(pos, ally);
                }
                else {
                    Debug.LogWarning("No ally on GameObject: " + startingAllies[i]);
                }
                
                Character enemy = startingEnemies[i].GetComponent<Character>();
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
        if (autoTurn) {
            turnTimer += Time.deltaTime;
            if (turnTimer >= turnTime) {
                turnTimer = 0f;

                NextTurn();
            }
        }
    }
    
    public Character GetFrontAlly() {
        var sorted = allies.OrderBy(kvp => kvp.Key).ToList();
        return sorted[0].Value;
    }
    
    public Character GetFrontEnemy() {
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
        Character ally = GetActiveAlly(positionCounter);
        Character enemy = GetActiveEnemy(positionCounter);

        if (ally != null && enemy != null) {
            DetermineTurnOrder(ally, enemy);
        }
    }

    private void DetermineTurnOrder(Character ally, Character enemy) {
        int allySpeed = ally.Speed;
        int enemySpeed = enemy.Speed;
        
        Debug.Log($"Ally speed: {allySpeed} | Enemy speed: {enemySpeed}");
        
        //  Ally / enemy turn order is based on speed
        // if (allySpeed > enemySpeed) {
        //     ally.TakeTurn(positionCounter);
        //     
        //     EvaluateRound();
        //     
        //     if (!enemy.IsDead()) {
        //         enemy.TakeTurn(positionCounter);
        //     }
        // }
        // else if (allySpeed == enemySpeed){
        //     //  Take turns simultaneous
        //     ally.TakeTurn(positionCounter);
        //     enemy.TakeTurn(positionCounter);
        // }
        // else {
        //     enemy.TakeTurn(positionCounter);
        //     
        //     EvaluateRound();
        //     
        //     if (!ally.IsDead()) {
        //         ally.TakeTurn(positionCounter);
        //     }
        // }
        //
        // EvaluateRound();
    }

    private void EvaluateRound() {
        //  Evaluate Health
        foreach (var ally in allies) {
            ally.Value.EvaluateHealth();
        }
        foreach (var enemy in enemies) {
            enemy.Value.EvaluateHealth();
        }
    }

    private Character GetActiveAlly(int position) {
        bool allyForPosition = allies.TryGetValue(position, out Character character);
        if (allyForPosition) {
            Debug.Log("Active Ally: " + character);
            return character;
        }

        Debug.LogWarning($"No ally for position {position}: {allies}");
        
        return null;
    }
    
    private Character GetActiveEnemy(int position) {
        bool enemyForPosition = enemies.TryGetValue(position, out Character character);
        if (enemyForPosition) {
            Debug.Log("Active Enemy: " + character);
            return character;
        }

        Debug.LogWarning($"No enemy for position {position}: {enemies}");
        
        return null;
    }
}
