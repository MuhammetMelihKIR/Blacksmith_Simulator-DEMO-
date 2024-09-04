using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
  {
      [SerializeField] private Camera playerCamera;
      [SerializeField] private Camera ProductionTableCamera;

      private void OnEnable()
      {
          CoreGameSignals.OnPlayerCameraChange += CameraChange;
      }

      private void OnDisable()
      {
          CoreGameSignals.OnPlayerCameraChange -= CameraChange;
      }

      private void Awake()
      {
          playerCamera.enabled = true;
          ProductionTableCamera.enabled = false;
      }

      private void CameraChange(bool isActive)
      {
          playerCamera.enabled = !isActive;
          ProductionTableCamera.enabled = isActive;
      }
      
  }
