using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour {
    public TMP_Text PowerText;
    public TMP_Text SpeedText;
    public TMP_Text HealthText;
    
    public void DisplayStatusValues(Character character) {
        PowerText.text = character.Power.ToString();
        SpeedText.text = character.Speed.ToString();
        HealthText.text = character.Health.ToString();
    }
}
