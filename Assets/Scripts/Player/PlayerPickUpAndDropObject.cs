using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpAndDropObject : MonoBehaviour
{
    #region References
    
    [Header("References")]
    [SerializeField] private Transform pickUpPoint;
    [SerializeField] private List<GameObject> pickUpObjectList= new List<GameObject>();
    
    private GameObject sampleObject;
    [SerializeField] private BlackSmithObjectSO blackSmithObjectSO;
    
    #endregion
    private void OnEnable()
    { 
        CoreGameSignals.OnTakeableObjectDetected += HandleTakeableObject;
        
    }

    private void OnDisable()
    {
        CoreGameSignals.OnTakeableObjectDetected -= HandleTakeableObject;
    }
    
    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        return blackSmithObjectSO;
    }

    private void HandleTakeableObject(ITakeable takeable)
    {
        if (pickUpObjectList.Count <= 0)
        {
            PickUpNewObject(takeable);
        }
        else
        {
            TryDropObject(takeable);
        }
    }

    private void PickUpNewObject(ITakeable takeable)
    {
        if (blackSmithObjectSO == null)
        { 
            blackSmithObjectSO = takeable.GetBlackSmithObjectSO();
            takeable.GetObject();
            
            sampleObject = Instantiate(takeable.GetPrefab());
            sampleObject.transform.position = pickUpPoint.position;
            sampleObject.transform.rotation = pickUpPoint.rotation;
            sampleObject.transform.parent = pickUpPoint;
    
            pickUpObjectList.Add(sampleObject);
            
        }
    }

    private void TryDropObject(ITakeable takeable)
    {
        if (blackSmithObjectSO == takeable.GetBlackSmithObjectSO() )
        {
            takeable.GiveObject();
            Destroy(pickUpObjectList[0]);
            pickUpObjectList.RemoveAt(0);
            blackSmithObjectSO = null;
        }
    }
    
}
