using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private float rayDistance = .75f;

    private void OnEnable()
    {
        CoreGameSignals.OnInteractObject += DrawRaycast;
    }
    

    private void DrawRaycast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.gameObject.TryGetComponent<ITakeable>(out ITakeable takeable))
            {
                CoreGameSignals.OnTakeable_ObjectDetected?.Invoke(takeable);
            }
        } 
    }


    private void OnDisable()
    {
        CoreGameSignals.OnInteractObject -= DrawRaycast;
    }
    
    

    
}
