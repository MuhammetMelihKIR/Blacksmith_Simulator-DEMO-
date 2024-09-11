using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public GameObject customerPrefab;
    public EquipmentListSO equipmentList;

}
