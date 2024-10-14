using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TurnBased
{
    public class BattleHUB : MonoBehaviour
    {
        public Text nameText;
        public Text levelText;
        public Image healthBar;
        public Image icon;

        public void SetHUB(Unit unit)
        { 
            nameText.text = unit.name;
            levelText.text = $"level: {unit.unitLevel}";
            icon.sprite = unit.unitIcon;
            SetHealth(unit);
        }
        public void SetHealth(Unit unit)
        {
            healthBar.fillAmount = Mathf.Clamp01(unit.currentHealth / unit.maxHealth);
        }
    }

}
