using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MaterialToMeltedSO : ScriptableObject
{
    public BlackSmithObjectSO inputMaterial;
    public BlackSmithObjectSO outputMaterial;
    public float meltTime;
    
}
