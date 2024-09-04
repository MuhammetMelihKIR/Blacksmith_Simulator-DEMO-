using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CoreGameSignals : MonoBehaviour
{   
    public UnityAction<bool> OnInteractObjectControl = delegate { };
    public UnityAction OnInteractObject = delegate { };
    public UnityAction<ITakeable> OnTakeableObjectDetected = delegate { };
    public UnityAction OnOutlineDeactive = delegate { };
    public UnityAction<GameObject> OnInstantiateObjectProductionTable = delegate { };
    
    
    
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
    
    
   
}
