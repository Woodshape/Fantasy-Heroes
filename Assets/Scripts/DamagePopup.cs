
using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamagePopup : MonoBehaviour {
    private TextMeshPro textMeshPro;

    [SerializeField]
    private float upSpeed = 5f;
    [SerializeField]
    private float lifeTimer = 2f;

    private void Awake() {
        textMeshPro = GetComponent<TextMeshPro>();
        upSpeed += Random.Range(-4f, 4f);
    }

    private void Update() {
        transform.position += new Vector3(0f, upSpeed) * Time.deltaTime;
        
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f) {
            Destroy(gameObject);
        }
    }

    public void Setup(int amount, bool isHeal = false) {
        textMeshPro.SetText(amount.ToString());
    }
}

