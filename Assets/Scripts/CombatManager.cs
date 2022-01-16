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

    public List<GameObject> allyPositions = new List<GameObject>();
    public List<GameObject> enemyPositions = new List<GameObject>();
    
    private Dictionary<int, Ally> allies = new Dictionary<int, Ally>();
    private Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();
    
    [SerializeField]
    // private int positions = 6;
    private int positionCounter;
    private int allyPosition;
    private int enemyPosition;
    
    [SerializeField]
    private float turnTime = 1;
    private float turnTimer;
    private float turnNumber;

    [SerializeField]
    private bool autoTurn;

    private bool combatOver;
    
    public event EventHandler RoundOverEvent;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        
        for (int i = 0; i < startingAllies.Length; i++) {
            int pos = i + 1;
            if (pos <= allyPositions.Count) {
                Ally ally = startingAllies[i].GetComponent<Ally>();
                if (ally != null) {
                    AddAlly(pos, ally);
                }
                else {
                    Debug.LogWarning("No ally on GameObject: " + startingAllies[i]);
                }
            }
        }
        
        for (int i = 0; i < startingEnemies.Length; i++) {
            int pos = i + 1;
            if (pos <= enemyPositions.Count) {
                Enemy enemy = startingEnemies[i].GetComponent<Enemy>();
                if (enemy != null) {
                    AddEnemy(pos, enemy);
                }
                else {
                    Debug.LogWarning("No enemy on GameObject: " + startingEnemies[i]);
                }
            }
        }
    }
    
    private void AddAlly(int pos, Ally ally) {
        Debug.Log($"Adding ally at position {pos}: {ally}");
        allies.Add(pos, ally);
        
        Instantiate(ally, allyPositions[pos - 1].transform.position, Quaternion.identity);

        ally.HealthChangedEvent += OnHealthChanged;
    }

    private void RemoveAlly(Ally ally) {
        ally.HealthChangedEvent -= OnHealthChanged;
        ally.Die();
    }
    
    private void AddEnemy(int pos, Enemy enemy) {
        Debug.Log($"Adding enemy at position {pos}: {enemy}");
        enemies.Add(pos, enemy);

        Instantiate(enemy, enemyPositions[pos - 1].transform.position, Quaternion.identity); 

        enemy.HealthChangedEvent += OnHealthChanged;
    }
    
    private void RemoveEnemy(Enemy enemy) {
        enemy.HealthChangedEvent -= OnHealthChanged;
        enemy.Die();
    }
    
    private void OnHealthChanged(object sender, EventArgs e) {
        Character character = (Character) sender;
        Debug.Log($"Health changed for {character.Name}: {character.Health}");
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

                NextRound();
            }
        }
    }
    
    public Ally GetFrontAlly() {
        if (allies.Count > 0) {
            var sorted = allies.OrderBy(kvp => kvp.Key).ToList();
            return sorted[0].Value;
        }

        Debug.LogWarning("No allies");
        return null;
    }
    
    public Enemy GetFrontEnemy() {
        if (enemies.Count > 0) {
            var sorted = enemies.OrderBy(kvp => kvp.Key).ToList();
            return sorted[0].Value;
        }
        
        Debug.LogWarning("No enemies");
        return null;
    }
    
    private void NextRound() {
        turnNumber++;
        allyPosition++;
        if (allyPosition > allyPositions.Count) {
            allyPosition = 1;
        }
        enemyPosition++;
        if (enemyPosition > enemyPositions.Count) {
            enemyPosition = 1;
        }

        Debug.Log($"Round: {turnNumber}");
        
        Ally ally = GetFrontAlly();
        Enemy enemy = GetFrontEnemy();

        if (ally != null && enemy != null) {
            DetermineTurnOrder(ally, enemy);
        }
        
        DetermineWinner();
    }

    private void DetermineTurnOrder(Ally ally, Enemy enemy) {
        int allySpeed = ally.Speed;
        int enemySpeed = enemy.Speed;
        
        Debug.Log($"Ally speed: {allySpeed} | Enemy speed: {enemySpeed}");
        
        //  Ally / enemy turn order is based on speed
        if (allySpeed > enemySpeed) {
            //  Ally acts first
            ally.TakeTurn(allyPosition);

            //  Enemy acts second, if still alive
            if (!enemy.IsDead()) {
                enemy.TakeTurn(enemyPosition);
            }
        }
        else if (allySpeed == enemySpeed){
            //  Take turns simultaneous
            ally.TakeTurn(allyPosition);
            enemy.TakeTurn(enemyPosition);
        }
        else {
            //  Enemy acts first
            enemy.TakeTurn(enemyPosition);
            
            //  Ally acts second, if still alive
            if (!ally.IsDead()) {
                ally.TakeTurn(allyPosition);
            }
        }
        
        EvaluateTurn();
    }

    private void EvaluateTurn() {
        List<KeyValuePair<int, Ally>> alliesToRemove = new List<KeyValuePair<int, Ally>>();
        foreach (var ally in allies) {
            if (ally.Value.IsDead()) {
                alliesToRemove.Add(ally);
                
                RemoveAlly(ally.Value);
            }
        }

        foreach (var ally in alliesToRemove) {
            allies.Remove(ally.Key);
        }

        List<KeyValuePair<int, Enemy>> enemiesToRemove = new List<KeyValuePair<int, Enemy>>();
        foreach (var enemy in enemies) {
            if (enemy.Value.IsDead()) {
                enemiesToRemove.Add(enemy);
                
                RemoveEnemy(enemy.Value);
            }
        }
        
        foreach (var enemy in enemiesToRemove) {
            enemies.Remove(enemy.Key);
        }
        
        OnRoundOver();
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
            Debug.Log("No winner: next round...");
        }
    }

    private Ally GetAlly(int position) {
        bool allyForPosition = allies.TryGetValue(position, out Ally character);
        if (allyForPosition) {
            Debug.Log("Active Ally: " + character);
            return character;
        }

        Debug.LogWarning($"No ally for position {position}: {allies}");
        
        return null;
    }
    
    private Enemy GetEnemy(int position) {
        bool enemyForPosition = enemies.TryGetValue(position, out Enemy character);
        if (enemyForPosition) {
            Debug.Log("Active Enemy: " + character);
            return character;
        }

        Debug.LogWarning($"No enemy for position {position}: {enemies}");
        
        return null;
    }
    
    protected virtual void OnRoundOver() {
        RoundOverEvent?.Invoke(this, EventArgs.Empty);
    }

    public List<Ally> GetAllies() {
        return new List<Ally>(allies.Values);
    }
    
    public List<Enemy> GetEnemies() {
        return new List<Enemy>(enemies.Values);
    }
}
