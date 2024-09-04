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
            CoreGameSignals.Instance.OnInteractObjectControl?.Invoke(true);
            takeable.OutlineActive();
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<ITakeable>(out ITakeable takeable))
        {
            CoreGameSignals.Instance.OnInteractObjectControl?.Invoke(false);
            CoreGameSignals.Instance.OnOutlineDeactive?.Invoke();
            
        }
    }
}
