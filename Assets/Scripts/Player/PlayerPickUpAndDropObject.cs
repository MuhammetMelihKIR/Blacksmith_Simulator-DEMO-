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
        CoreGameSignals.OnTakeable_ObjectDetected += HandleTakeableObject;
    }
    private void OnDisable()
    {
        CoreGameSignals.OnTakeable_ObjectDetected -= HandleTakeableObject;
    }
    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        return blackSmithObjectSO;
    }

    private void HandleTakeableObject(BlackSmithObjectSO smithObjectSo,ITakeable takeable)
    {
        if (pickUpObjectList.Count <= 0)
        {
            PickUpNewObject(smithObjectSo,takeable);
        }
        else
        {
            TryDropObject(smithObjectSo,takeable);
        }
    }
    private void PickUpNewObject(BlackSmithObjectSO smithObjectSo,ITakeable takeable)
    {
        if (blackSmithObjectSO == null)
        { 
            blackSmithObjectSO = smithObjectSo;
            takeable.GetObject();
            
            sampleObject = Instantiate(takeable.GetPrefab());
            sampleObject.transform.position = pickUpPoint.position;
            sampleObject.transform.rotation = pickUpPoint.rotation;
            sampleObject.transform.parent = pickUpPoint;
    
            pickUpObjectList.Add(sampleObject);
            
        }
    }

    private void TryDropObject(BlackSmithObjectSO smithObjectSo,ITakeable takeable)
    {
        if (blackSmithObjectSO == smithObjectSo )
        {
            takeable.GiveObject();
            Destroy(pickUpObjectList[0]);
            pickUpObjectList.RemoveAt(0);
            blackSmithObjectSO = null;
        }
    }


    
    
}
