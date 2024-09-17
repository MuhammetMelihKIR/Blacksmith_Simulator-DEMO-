using UnityEngine;

[CreateAssetMenu]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public GameObject customerPrefab;
    public EquipmentListSO equipmentList;

}
