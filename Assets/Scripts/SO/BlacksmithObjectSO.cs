using UnityEngine;

[CreateAssetMenu]
public class BlacksmithObjectSO : ScriptableObject
{
    public GameObject prefab;
    
    [Header("Canvas")]
    public string ObjectName;
    public Sprite ObjectSprite;
}
