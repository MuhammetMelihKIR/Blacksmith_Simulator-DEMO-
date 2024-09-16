using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private float rayDistance = 2f;
    private void OnEnable()
    {
        CoreGameSignals.OnInteractObject += DrawRaycast;
    }
    private void DrawRaycast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 0.5f);
            if (hit.collider.gameObject.TryGetComponent<ITakeable>(out ITakeable takeable))
            {
                CoreGameSignals.Takeable_OnObjectDetected?.Invoke( takeable);
            }
            else if (hit.collider.gameObject.TryGetComponent<IGetInteractable>(out IGetInteractable getInteractable))
            {
                getInteractable.GetInteract();
            }
        } 
    }
    private void OnDisable()
    {
        CoreGameSignals.OnInteractObject -= DrawRaycast;
    }
}
