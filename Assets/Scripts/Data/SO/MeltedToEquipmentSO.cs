using UnityEngine;
[CreateAssetMenu]
public class MeltedToEquipmentSO : ScriptableObject
{
    [Header("Forging")]
    public BlacksmithObjectSO inputObject;
    public BlacksmithObjectSO outputObject;
    public float forgingTime;
}
