using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class BlackSmithObjectSO : ScriptableObject
{
    public GameObject prefab;
    
    [Header("Canvas")]
    public string ObjectName;
    public Sprite ObjectSprite;
}
