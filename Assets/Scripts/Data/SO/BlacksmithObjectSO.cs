using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class BlacksmithObjectSO : ScriptableObject
{
    public GameObject prefab;
    
    [Header("Price")]
    public int purchasePrice;
    public int salesPrice;
    
    [Header("Canvas")]
    public string objectName;
    public Sprite objectSprite;
}
