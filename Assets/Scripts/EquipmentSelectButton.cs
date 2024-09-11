using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectButton : MonoBehaviour
{
    [SerializeField] private MeltedToEquipmentSO meltedToEquipmentSO;
    [Header("REFERENCES")]
    [SerializeField] private Image equipmentSprite;
    [SerializeField] private TextMeshProUGUI equipmentNameText;
    [SerializeField] private Material prefabMaterial;

    public MeltedToEquipmentSO GetMeltedToEquipmentSO()
    {
        return meltedToEquipmentSO; 
    }
    private void Awake()
    {
        equipmentSprite.sprite = meltedToEquipmentSO.outputObject.objectSprite;
        equipmentNameText.text = meltedToEquipmentSO.outputObject.objectName;
    }
    
    private void GetPrefab() //BUTTON CLICK
    {
        CoreGameSignals.OnProductionTable_InstantiateObjectForForging?.Invoke(meltedToEquipmentSO.outputObject,prefabMaterial);
    }
    
}
