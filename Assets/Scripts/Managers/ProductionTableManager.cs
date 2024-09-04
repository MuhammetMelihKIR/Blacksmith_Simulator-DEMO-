using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionTableManager : MonoBehaviour, IGetInteractable
{
    [SerializeField] private Material prefabMaterial;
    [SerializeField] private Transform instantiatePoint;
    [SerializeField] private List<GameObject> equipmentList = new List<GameObject>();

    [Header("UI")] 
    [SerializeField] private Canvas productionCanvas;
    [SerializeField] private GameObject equipmentListPanel;
    [SerializeField] private GameObject forgingSliderPanel;
    [SerializeField] private Button button;
    
    private Outline outline;

    #region IGetInteractable INTERFACE
    
    public void GetInteract()
    {
        CoreGameSignals.OnPlayerCameraChange?.Invoke(true);
        CoreUISignals.OnProductionTable_CanvasIsActive?.Invoke(true);
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive?.Invoke(true);
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive?.Invoke(false);
        OnOutlineDeactive();
        
    }

    public void OutlineActive()
    {
        outline.enabled = true;
    }
    

    #endregion

    #region ON ENABLE/DISABLE

    private void OnEnable()
    {
        CoreGameSignals.OnOutlineDeactive += OnOutlineDeactive;
        CoreGameSignals.OnInstantiateObjectProductionTable += InstantiateObject;
        
        CoreUISignals.OnProductionTable_CanvasIsActive += OnCanvasIsActive;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive += OnEquipmentListPanelIsActive;
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive += OnForgingSliderPanelIsActive;
    }
    
    private void OnOutlineDeactive()
    {
        outline.enabled = false;
    }
    
    private void InstantiateObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab , instantiatePoint.position , prefab.transform.rotation);
        obj.transform.parent = instantiatePoint;
        obj.GetComponentInChildren<MeshRenderer>().material = prefabMaterial;
        if (equipmentList.Count >=1)
        {
            Destroy( equipmentList[0]);
            equipmentList.RemoveAt(0);
        }
        equipmentList.Add(obj);
        
    }
    
    #region UI 

    private void OnCanvasIsActive(bool isActive)
    {
        productionCanvas.gameObject.SetActive(isActive);
    }
    
    private void OnEquipmentListPanelIsActive(bool isActive)
    {
        equipmentListPanel.SetActive(isActive);
    }
    
    private void OnForgingSliderPanelIsActive(bool isActive)
    {
        forgingSliderPanel.SetActive(isActive);
    }

    #endregion

    private void OnDisable()
    {
        CoreGameSignals.OnOutlineDeactive -= OnOutlineDeactive;
        CoreGameSignals.OnInstantiateObjectProductionTable -= InstantiateObject;
        
        CoreUISignals.OnProductionTable_CanvasIsActive -= OnCanvasIsActive;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive -= OnEquipmentListPanelIsActive;
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive -= OnForgingSliderPanelIsActive;
    }

    #endregion
    

    private void Awake()
    {
        outline = GetComponent<Outline>();
        
    }
    
    
    
    
    
    
    

   
    
    
}
