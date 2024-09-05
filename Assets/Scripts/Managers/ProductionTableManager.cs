using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionTableManager : MonoBehaviour, ITakeable
{
    [SerializeField] private Material prefabMaterial;
    [SerializeField] private Transform instantiatePoint;
    [SerializeField] private List<GameObject> equipmentList = new List<GameObject>();
    private BlackSmithObjectSO blackSmithObjectSO;
    
    private bool isInstantiated = false;
    private int hammerHitNumber;

    [Header("UI")] 
    [SerializeField] private Canvas productionCanvas;
    [SerializeField] private GameObject equipmentListPanel;
    [SerializeField] private GameObject forgingSliderPanel;
    [SerializeField] private Button equipmentListCloseButton;
    
    private Outline outline;

    #region IGetInteractable INTERFACE
    
    public void GetInteract()
    {
        CoreGameSignals.OnPlayerCameraChange?.Invoke(true);
        CoreGameSignals.OnPlayerCanMove?.Invoke(false);
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.None);
        
        CoreUISignals.OnProductionTable_CanvasIsActive?.Invoke(true);
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive?.Invoke(true);
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive?.Invoke(false);
        
        OnOutlineDeactive();
    }
    
    public void GetObject()
    {
        
    }

    public void GiveObject()
    {
        
    }
    
    public GameObject GetPrefab()
    {
        return blackSmithObjectSO.prefab;
    }
    
    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        return blackSmithObjectSO;
    }

    public void OutlineActive()
    {
        outline.enabled = true;
    }

    #endregion

    #region ON ENABLE/DISABLE

    private void OnEnable()
    {
        CoreGameSignals.OnOutline_Deactive += OnOutlineDeactive;
        CoreGameSignals.OnProductionTable_InstantiateObject += InstantiateObjectForForging;
        CoreGameSignals.OnProductionTable_HammerHit += OnHammerHit;
        
        //UI
        CoreUISignals.OnProductionTable_CanvasIsActive += OnCanvasIsActive;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive += OnEquipmentListPanelIsActive;
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive += OnForgingSliderPanelIsActive;
    }
    
    private void OnOutlineDeactive()
    {
        outline.enabled = false;
    }
    
    private void InstantiateObjectForForging(BlackSmithObjectSO SO)
    {
        if (equipmentList.Count >=1)
        {
            Destroy( equipmentList[0]);
            equipmentList.RemoveAt(0);
        }
        blackSmithObjectSO = SO;
        GameObject prefab = blackSmithObjectSO.prefab;
        GameObject forgingObject = Instantiate(prefab, instantiatePoint.position , prefab.transform.rotation);
        forgingObject.transform.parent = instantiatePoint;
        forgingObject.GetComponentInChildren<MeshRenderer>().material = prefabMaterial;
        
        equipmentList.Add(forgingObject);
        
        isInstantiated = true;
    }
    private void OnHammerHit()
    {
        if (hammerHitNumber>=5)
        {
            CoreGameSignals.OnPlayerCameraChange?.Invoke(false);
            CoreGameSignals.OnPlayerCanMove?.Invoke(true);
            CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
            CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive?.Invoke(false);
            hammerHitNumber = 0;
        }
        hammerHitNumber++;
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
        CoreGameSignals.OnOutline_Deactive -= OnOutlineDeactive;
        CoreGameSignals.OnProductionTable_InstantiateObject -= InstantiateObjectForForging;
        CoreGameSignals.OnProductionTable_HammerHit -= OnHammerHit;
        
        //UI
        CoreUISignals.OnProductionTable_CanvasIsActive -= OnCanvasIsActive;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive -= OnEquipmentListPanelIsActive;
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive -= OnForgingSliderPanelIsActive;
    }

    #endregion
    

    private void Awake()
    {
        outline = GetComponent<Outline>();
        
        equipmentListCloseButton.onClick.AddListener(CloseEquipmentPanel);
    }
    
    private void CloseEquipmentPanel() //BUTTON CLICK
    {
        if (!isInstantiated)  return;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive?.Invoke(false);
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive?.Invoke(true);
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
    }
    
}
