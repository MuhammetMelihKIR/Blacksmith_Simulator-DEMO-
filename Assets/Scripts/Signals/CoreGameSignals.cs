using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CoreGameSignals : MonoBehaviour
{   
    #region Singleton
    private static CoreGameSignals _instance;
    public static CoreGameSignals Instance { get { return _instance; }
    }

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }
        _instance = this;
        
    }
    #endregion

    // PLAYER
    
    public static UnityAction<bool> OnInteractObjectControl = delegate { }; // PlAYER TRIGGER CHECK
    public static UnityAction OnInteractObject = delegate { }; // PLAYER INTERACT
    public static UnityAction<bool> OnPlayerCameraChange = delegate { }; // PLAYER CAMERA CHANGE
    public static UnityAction<CursorLockMode> OnCursorLockState = delegate { }; // CURSOR LOCK
    public static UnityAction<bool> OnPlayerCanMove = delegate { }; // PLAYER CAN MOVE
    
    public static UnityAction OnPlayerPickUpAndDropObject_PickUpListRemove = delegate { }; // PLAYER PICK UP AND DROP OBJECT
    
    // OUTLINE
    
    public static UnityAction OnOutline_Deactive = delegate { }; // OUTLINE DEACTIVE
    
    // INTERFACE
    
    public static UnityAction<ITakeable> OnTakeable_ObjectDetected = delegate { }; // ITAKEABLE INTERFACE CONTROL 
    
    // PRODUCTION TABLE
    
    public static UnityAction<BlacksmithObjectSO,Material> OnProductionTable_InstantiateObjectForForging = delegate { }; // INSTANTIATE NON MATERIAL OBJECT---PRODUCTION TABLE
    public static UnityAction OnProductionTable_HammerHit = delegate { }; // PRODUCTION TABLE HAMMER HIT
    
    // OvenManger
    
    public static UnityAction<bool> OnOvenManager_IsMelted = delegate { }; // OVEN MANAGER
    
    // CustomerManager
    
    public static UnityAction OnCustomerManager_ProcessCustomerInQueue = delegate { }; // CUSTOMER MANAGER
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   
}
