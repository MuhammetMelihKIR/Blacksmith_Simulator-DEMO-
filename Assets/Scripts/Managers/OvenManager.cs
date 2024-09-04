using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenManager : MonoBehaviour, ITakeable
{
    private Outline outline;
    
    [SerializeField] private MaterialToMeltedSO[] materialToMeltedSO;
    [SerializeField] private PlayerPickUpAndDropObject playerPickUpAndDropObject;
    public BlackSmithObjectSO blackSmithObjectSO;
    
    
    
    #region ITakeable INTERFACE

    public void GetObject()
    {
        
    }

    public void GiveObject()
    {
        MeltToMaterial();
    }
    
    public GameObject GetPrefab()
    {
        return blackSmithObjectSO.prefab;
    }
    
    public void OutlineActive()
    {
       outline.enabled = true;
    }
    
    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        return playerPickUpAndDropObject.GetBlackSmithObjectSO();
    }

    #endregion

    
    #region ON ENABLE AND DISABLE

    private void OnEnable()
    {
        CoreGameSignals.Instance.OnOutlineDeactive += OnOutlineDeactive;
    }
    
    private void OnOutlineDeactive()
    {
        outline.enabled = false;
    }

    private void OnDisable()
    {
        CoreGameSignals.Instance.OnOutlineDeactive -= OnOutlineDeactive;
    }

    #endregion

    private void MeltToMaterial()
    {
        foreach (MaterialToMeltedSO obj in materialToMeltedSO)
        {
            if (obj.inputMaterial == playerPickUpAndDropObject.GetBlackSmithObjectSO())
            {
                blackSmithObjectSO = obj.outputMaterial;
                
            }
        }
    }
    

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
}
