using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MeltedToEquipmentSO : ScriptableObject
{
    [Header("Forging")]
    public BlackSmithObjectSO inputObject;
    public BlackSmithObjectSO outputObject;
    public float forgingTime;

}
