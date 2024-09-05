using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ITakeable>(out ITakeable takeable))
        { 
            CoreGameSignals.OnInteractObjectControl?.Invoke(true);
            takeable.OutlineActive();
            takeable.GetInteract();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<ITakeable>(out ITakeable takeable))
        {
            CoreGameSignals.OnInteractObjectControl?.Invoke(false);
            CoreGameSignals.OnOutline_Deactive?.Invoke();
            
        }
    }
}
