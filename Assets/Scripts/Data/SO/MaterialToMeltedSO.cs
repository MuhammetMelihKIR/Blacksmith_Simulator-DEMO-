using UnityEngine;

[CreateAssetMenu]
public class MaterialToMeltedSO : ScriptableObject
{
    public BlacksmithObjectSO inputMaterial;
    public BlacksmithObjectSO outputMaterial;
    public float meltTime;
    
}
