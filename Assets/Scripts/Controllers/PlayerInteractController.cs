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
        }
        else if(other.gameObject.TryGetComponent<TrashManager>(out TrashManager trashManager))
        {
            CoreGameSignals.OnInteractObjectControl?.Invoke(true);
        }
        else if(other.gameObject.TryGetComponent<IGetInteractable>(out IGetInteractable getInteractable))
        {
            getInteractable.OutlineActive();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<ITakeable>(out ITakeable takeable))
        {
            CoreGameSignals.OnInteractObjectControl?.Invoke(false);
            takeable.OutlineDeactive();
            
        }
        else if(other.gameObject.TryGetComponent<TrashManager>(out TrashManager trashManager))
        {
            CoreGameSignals.OnInteractObjectControl?.Invoke(false);
        }
        else if(other.gameObject.TryGetComponent<IGetInteractable>(out IGetInteractable getInteractable))
        {
            getInteractable.OutlineDeactive();
        }
    }
}
