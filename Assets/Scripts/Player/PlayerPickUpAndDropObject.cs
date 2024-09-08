using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        CoreGameSignals.OnPlayerPickUpAndDropObject_PickUpListRemove += OnPickUpObjectListRemove;
    }

    private void OnPickUpObjectListRemove()
    {
        blackSmithObjectSO = null;
        Destroy(sampleObject);
        pickUpObjectList.RemoveAt(0);
    }
    private void OnDisable()
    {
        CoreGameSignals.OnTakeable_ObjectDetected -= HandleTakeableObject;
        CoreGameSignals.OnPlayerPickUpAndDropObject_PickUpListRemove -= OnPickUpObjectListRemove;
    }
    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        return blackSmithObjectSO;
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
        if (blackSmithObjectSO == null)
        {
            blackSmithObjectSO = takeable.GetBlackSmithObjectSO();
            sampleObject = Instantiate(takeable.GetPrefab());
            sampleObject.transform.position = pickUpPoint.position;
            sampleObject.transform.rotation = pickUpPoint.rotation;
            sampleObject.transform.parent = pickUpPoint;
            pickUpObjectList.Add(sampleObject);
            
            takeable.GetObject();
        }
    }

    private void TryDropObject(ITakeable takeable)
    {
        if (blackSmithObjectSO == takeable.GetBlackSmithObjectSO())
        {
            takeable.GiveObject();
            
            Destroy(pickUpObjectList[0]);
            pickUpObjectList.RemoveAt(0);
            blackSmithObjectSO = null;
        }
    }
    
}
