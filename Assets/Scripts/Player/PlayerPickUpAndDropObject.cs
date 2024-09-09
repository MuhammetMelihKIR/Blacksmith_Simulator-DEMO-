using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPickUpAndDropObject : MonoBehaviour
{
    #region References
    [Header("References")]
    [SerializeField] private Transform pickUpPoint;
    [SerializeField] private List<GameObject> pickUpObjectList= new List<GameObject>();
    
    private GameObject sampleObject;
    [FormerlySerializedAs("blackSmithObjectSO")] [SerializeField] private BlacksmithObjectSO blacksmithObjectSo;
    
    #endregion
    private void OnEnable()
    { 
        CoreGameSignals.OnTakeable_ObjectDetected += HandleTakeableObject;
        CoreGameSignals.OnPlayerPickUpAndDropObject_PickUpListRemove += OnPickUpObjectListRemove;
    }

    private void OnPickUpObjectListRemove()
    {
        blacksmithObjectSo = null;
        Destroy(sampleObject);
        pickUpObjectList.RemoveAt(0);
    }
    private void OnDisable()
    {
        CoreGameSignals.OnTakeable_ObjectDetected -= HandleTakeableObject;
        CoreGameSignals.OnPlayerPickUpAndDropObject_PickUpListRemove -= OnPickUpObjectListRemove;
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
