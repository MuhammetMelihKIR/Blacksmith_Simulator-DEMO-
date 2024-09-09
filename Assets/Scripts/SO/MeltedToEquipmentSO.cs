using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MeltedToEquipmentSO : ScriptableObject
{
    [Header("Forging")]
    public BlacksmithObjectSO inputObject;
    public BlacksmithObjectSO outputObject;
    public float forgingTime;

}
