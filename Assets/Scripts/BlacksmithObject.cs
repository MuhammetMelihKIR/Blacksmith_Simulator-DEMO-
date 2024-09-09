using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BlacksmithObject : MonoBehaviour
{   
    [FormerlySerializedAs("blackSmithObjectSO")] [SerializeField] private BlacksmithObjectSO blacksmithObjectSo;

    public BlacksmithObjectSO GetBlackSmithObjectSO()
    {
        return blacksmithObjectSo;
    }
}
