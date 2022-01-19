using System;
using System.Collections.Generic;
using Data.Abilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data {
    public class Character : MonoBehaviour {
        public string Name;
        
        public int Power;
        public int Health;
        public int Speed;
    
        public int Armor;

        public DamagePopup DamagePopup;
    
        public List<GameObject> Buffs;
        public List<GameObject> Conditions;

        public List<Ability> Abilities;
        public IReaction Reaction;

        public AttackInformation AttackInformation;
    
        public event EventHandler HealthChangedEvent;

        public virtual void Setup(){}
    
        public virtual void TakeTurn(int position) {
            Debug.Log($"Taking turn at position {position}: {this}", this);
            Debug.Log("Front ally: " + CombatManager.Instance.GetFrontAlly(), this);
            Debug.Log("Front enemy: " + CombatManager.Instance.GetFrontEnemy(), this);

            AttackInformation = null;
        }

        public void RandomizeStats() {
            Power = Random.Range(1, 4);
            Speed = Random.Range(1, 4);
            Health = Random.Range(1, 4);
    
            Armor = Random.Range(0, 3);
        }
    
        public void TakeDamage(Character source, int amount, bool ignoreArmor, bool heavyArmor = false) {
            Debug.Log($"{Name} taking {amount} damage from {source}!", this);
    
            int damage = Math.Abs(amount);
            if (!ignoreArmor) {
                damage = Mathf.Clamp(damage - Armor, heavyArmor ? 0 : 1, 99);
            
                Debug.Log($"{Name} damage reduced by {Armor} armor: {damage}!", this);
            }
    
            Health -= damage;

            if (Health <= 0) {
                Debug.Log($"{Name} dies!", this);
                AttackInformation = new AttackInformation(source){IsDead = true};
            }
            else {
                Debug.Log($"{Name} is hurt!", this);
                AttackInformation = new AttackInformation(source){IsHurt = true};
            }
            
            DamagePopup damagePopup = Instantiate(DamagePopup, transform.position, Quaternion.identity);
            damagePopup.Setup(damage);

            OnHealthChanged();
        }
    
        public void Destroy() {
            Debug.Log($"{Name} is destroyed!", this);
            Destroy(gameObject);
        }

        public bool IsHurt() {
            return AttackInformation is {IsHurt: true};
        }
    
        public bool IsDead() {
            return AttackInformation is {IsDead: true};
        }
    
        public virtual bool CanReact(TriggerType triggerType) {
            return Reaction != null && Reaction.GetTriggerType().Equals(triggerType);
        }
    
        public override string ToString() {
            return $"{Name} -> P:{Power} | S:{Speed} | H:{Health} -> A:{Armor}";
        }
        
        protected virtual void OnHealthChanged() {
            HealthChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}

