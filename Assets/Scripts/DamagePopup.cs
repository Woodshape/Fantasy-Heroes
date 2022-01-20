
using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamagePopup : MonoBehaviour {
    private TextMeshPro textMeshPro;

    [SerializeField]
    private float upSpeed = 2f;
    [SerializeField]
    private float lifeTimer = 1f;
    [SerializeField]
    private float disappearSpeed = 3f;
    
    private const float DISAPPEAR_TIMER = 1f;

    private TextType type;
    private Color textColor;

    private void Awake() {
        textMeshPro = GetComponent<TextMeshPro>();
        
    }

    private void Update() {
        transform.position += new Vector3(0f, upSpeed) * Time.deltaTime;

        if (lifeTimer > DISAPPEAR_TIMER * 0.5f) {
            transform.localScale += (Vector3.one * 1f * Time.deltaTime);
        }
        else {
            transform.localScale -= (Vector3.one * 1f * Time.deltaTime);
        }
        
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f) {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMeshPro.color = textColor;

            if (textColor.a <= 0) {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(int amount, TextType type, bool isCritical = false) {
        textMeshPro.SetText(amount.ToString());
        this.type = type;
        
        if (isCritical) {
            textMeshPro.fontSize *= 1.5f;
            textColor = GetTextColor(type);
        }
        else {
            textColor = GetTextColor(type);
        }

        textMeshPro.color = textColor;

        lifeTimer = DISAPPEAR_TIMER;
    }
    
    private Color GetTextColor(TextType type) {
        Color color = new Color();
        switch (type) {
            case TextType.Damage:
                color = Color.red;
                break;
            case TextType.Heal:
                color = Color.green;
                break;
        }

        return color;
    }

    public enum TextType {
        Damage,
        Heal
    }
}

