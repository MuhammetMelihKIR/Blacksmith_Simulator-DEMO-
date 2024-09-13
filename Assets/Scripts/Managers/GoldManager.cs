using System;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    private int gold;
    [SerializeField] private TextMeshProUGUI goldText;

    private void OnEnable()
    {
     
        CoreGameSignals.GoldManager_OnGoldUpdate += OnGoldUpdate;
    }
    private void OnDisable()
    {
        CoreGameSignals.GoldManager_OnGoldUpdate -= OnGoldUpdate;
    }
    private void Start()
    {
        gold = 500;
        goldText.text = gold.ToString();
    }
    private void UpdateGoldText()
    {
        goldText.text = gold.ToString();
    } 
    private int GetGold()
    {
        return gold;
    }
    private void OnGoldUpdate(int amount)
    {
        if (amount < 0 && Mathf.Abs(amount) > gold) return;
        gold += amount;
        UpdateGoldText();
    }
}
