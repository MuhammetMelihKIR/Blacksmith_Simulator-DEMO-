using System;
using System.Collections;
using System.Collections.Generic;
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

    
    public static UnityAction<bool> OnInteractObjectControl = delegate { };
    
    public static UnityAction OnInteractObject = delegate { };
    
    public static UnityAction<ITakeable> OnTakeableObjectDetected = delegate { };
    
    public static UnityAction OnOutlineDeactive = delegate { };
    
    public static UnityAction<GameObject> OnInstantiateObjectProductionTable = delegate { };
    
    public static UnityAction<bool> OnPlayerCameraChange = delegate { };
    
   
}
