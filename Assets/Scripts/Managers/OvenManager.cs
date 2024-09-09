using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenManager : MonoBehaviour, ITakeable
{
    [SerializeField] private OvenManagerState currentState;
    private Outline outline;
    
    [SerializeField] private MaterialToMeltedSO[] materialToMeltedSO;
    [SerializeField] private PlayerPickUpAndDropObject playerPickUpAndDropObject;
    public BlackSmithObjectSO blackSmithObjectSO;
    
    #region ITakeable INTERFACE
    
    public void GetObject()
    {
        if (currentState != OvenManagerState.melted) return;
        
        blackSmithObjectSO = null;
        currentState = OvenManagerState.start;
    }

    public void GiveObject()
    {
        if (currentState != OvenManagerState.start) return;
        
        MeltToMaterial();
    }
    
    public GameObject GetPrefab()
    {
        if (currentState == OvenManagerState.melting) return null;
       
        return blackSmithObjectSO.prefab;
    }
    
    public void OutlineActive()
    {
       outline.enabled = true;
    }
    
    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        if (currentState == OvenManagerState.melting) return null;
        
        foreach (MaterialToMeltedSO obj in materialToMeltedSO)
        {
            if (obj.inputMaterial==playerPickUpAndDropObject.GetBlackSmithObjectSO())
            {
                blackSmithObjectSO = playerPickUpAndDropObject.GetBlackSmithObjectSO();
                return blackSmithObjectSO;
            }
        }

        return blackSmithObjectSO;
    }

    #endregion

    
    #region ON ENABLE AND DISABLE

    private void OnEnable()
    {
        CoreGameSignals.OnOutline_Deactive += OnOutlineDeactive;
    }
    private void OnOutlineDeactive()
    {
        outline.enabled = false;
    }
    private void OnDisable()
    {
        CoreGameSignals.OnOutline_Deactive -= OnOutlineDeactive;
    }
    #endregion

    private void MeltToMaterial()
    {
        foreach (MaterialToMeltedSO obj in materialToMeltedSO)
        {
            if (obj.inputMaterial == playerPickUpAndDropObject.GetBlackSmithObjectSO())
            {
                blackSmithObjectSO = obj.outputMaterial;
                currentState = OvenManagerState.melting;
                CoreGameSignals.OnOvenManager_IsMelted?.Invoke(false);
            }
        }
    }
    private void Awake()
    {
        outline = GetComponent<Outline>();
        currentState = OvenManagerState.start;
    }
    
    public OvenManagerState CurrentState(OvenManagerState state)
    {
        currentState = state;
        return currentState;
    }
    
}
