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
        else if(other.gameObject.TryGetComponent<IGetInteractable>(out IGetInteractable getInteractable))
        {
            CoreGameSignals.OnInteractObjectControl?.Invoke(true);
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
        else if(other.gameObject.TryGetComponent<IGetInteractable>(out IGetInteractable getInteractable))
        {
            CoreGameSignals.OnInteractObjectControl?.Invoke(false);
            getInteractable.OutlineDeactive();
        }
    }
}
