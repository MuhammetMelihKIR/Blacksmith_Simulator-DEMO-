using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectButton : MonoBehaviour
{
 
    [SerializeField] private BlackSmithObjectSO blackSmithObjectSO;
    
    [Header("REFERENCES")]
    [SerializeField] private Image equipmentSprite;
    [SerializeField] private TextMeshProUGUI equipmentNameText;
    
    private void Awake()
    {
        equipmentSprite.sprite = blackSmithObjectSO.ObjectSprite;
        equipmentNameText.text = blackSmithObjectSO.ObjectName;
    }
    
    public void GetPrefab() //BUTTON CLICK
    {
        CoreGameSignals.OnProductionTable_InstantiateObject?.Invoke(blackSmithObjectSO);
    }
    
    
}
