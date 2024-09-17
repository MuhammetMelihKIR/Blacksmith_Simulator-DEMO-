using System.Collections.Generic;
using UnityEngine;
public class PlayerPickUpAndDropObject : MonoBehaviour
{
    #region References
    [Header("References")]
    [SerializeField] private Transform pickUpPoint;
    [SerializeField] private List<GameObject> pickUpObjectList= new List<GameObject>();
    
    private GameObject sampleObject;
    [SerializeField] private BlacksmithObjectSO blacksmithObjectSo;
    
    #endregion
    private void OnEnable()
    { 
        CoreGameSignals.Takeable_OnObjectDetected += HandleTakeableObject;
        
        CoreGameSignals.PlayerPickUpAndDropObject_OnPickUpListRemove += OnPickUpObjectListRemove;
    }
    private void OnPickUpObjectListRemove()
    {
        blacksmithObjectSo = null;
        Destroy(sampleObject);
        pickUpObjectList.Remove(sampleObject);
    }
    private void OnDisable()
    {
        CoreGameSignals.Takeable_OnObjectDetected -= HandleTakeableObject;
        
        CoreGameSignals.PlayerPickUpAndDropObject_OnPickUpListRemove -= OnPickUpObjectListRemove;
    }
    public BlacksmithObjectSO GetBlackSmithObjectSO()
    {
        return blacksmithObjectSo;
    }
    private void HandleTakeableObject(ITakeable takeable)
    {
        if (pickUpObjectList.Count <= 0)
        {
            PickUpNewObject( takeable);
        }
        else
        {
            TryDropObject( takeable);
        }
    }
    private void PickUpNewObject(ITakeable takeable)
    {
       if (blacksmithObjectSo == null)
       {
           blacksmithObjectSo = takeable.GetBlackSmithObjectSO();
           
           if (takeable.GetPrefab()!=null)
           {
               sampleObject = Instantiate(takeable.GetPrefab());
               sampleObject.transform.position = pickUpPoint.position;
               sampleObject.transform.rotation = pickUpPoint.rotation;
               sampleObject.transform.parent = pickUpPoint;
               pickUpObjectList.Add(sampleObject);
           
               takeable.GetObject();
           }
       }
    }
    private void TryDropObject(ITakeable takeable)
    {
        if (blacksmithObjectSo == takeable.GetBlackSmithObjectSO())
        {
            takeable.GiveObject();
            Destroy(pickUpObjectList[0]);
            pickUpObjectList.RemoveAt(0);
            blacksmithObjectSo = null;
        }
    }
    
}
