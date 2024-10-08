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
    public static CoreGameSignals Instance { get { return _instance; } }

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
    
    
    // GAME STATE
    
    public static UnityAction<GameState> GameState_OnStateChange = delegate { };
    
    // INPUT
    
    public static UnityAction PlayerController_Forge = delegate { };

    // PLAYER
    
    public static UnityAction<bool> OnInteractObjectControl = delegate { }; // PlAYER TRIGGER CHECK
    public static UnityAction OnInteractObject = delegate { }; // PLAYER INTERACT
    public static UnityAction<bool> OnPlayerCameraChange = delegate { }; // PLAYER CAMERA CHANGE
    public static UnityAction<CursorLockMode> OnCursorLockState = delegate { }; // CURSOR LOCK
    public static UnityAction<bool> OnPlayerCanMove = delegate { }; // PLAYER CAN MOVE
    
    public static UnityAction<bool> Player_OnPlayerCameraRotate = delegate { }; // PLAYER CAN ROTATE

    // PLAYER PICK UP AND DROP OBJECT
    
    public static UnityAction PlayerPickUpAndDropObject_OnPickUpListRemove = delegate { }; // PLAYER PICK UP AND DROP OBJECT
    
    // CUSTOMER AI ANIMATION
    
    public static UnityAction CustomerAI_OnWalkingAnimation = delegate { };
    public static UnityAction CustomerAI_OnIdleAnimation = delegate { };
   
    // INTERFACE
    
    public static UnityAction<ITakeable> Takeable_OnObjectDetected = delegate { }; // ITAKEABLE INTERFACE CONTROL 
    
    // PRODUCTION TABLE
    
    public static UnityAction<BlacksmithObjectSO,Material> OnProductionTable_InstantiateObjectForForging = delegate { }; // INSTANTIATE NON MATERIAL OBJECT---PRODUCTION TABLE
    public static UnityAction ProductionTable_OnHammerHit = delegate { }; // PRODUCTION TABLE HAMMER HIT
    
    // OvenManger
    
    public static UnityAction<bool> OvenManager_OnIsMelted = delegate { }; // OVEN MANAGER
    
    // CustomerManager
    
    public static UnityAction CustomerManager_OnProcessCustomerInQueue = delegate { }; // CUSTOMER MANAGER
    
    // DealerManager 
    
    public static UnityAction<int> DealerManager_OnTotalPriceUpdate = delegate { };
    public static UnityAction DealerManager_OnReceiveOrder = delegate { };
    
    // DealerOrderButton
    
    public static UnityAction DealerOrderButton_OnPieceReset = delegate { };
    
    // GoldManager
    
    public static UnityAction<int> GoldManager_OnGoldUpdate = delegate { };
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   
}
