using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProductionTableManager : MonoBehaviour, ITakeable
{
    [SerializeField] private ProductionState currentState;
    
    [SerializeField] private PlayerPickUpAndDropObject playerPickUpAndDropObject;
    
    [Header("FORGING")]
    [SerializeField] private Transform instantiatePoint;
    [SerializeField] private List<GameObject> forgingList = new List<GameObject>();
    [SerializeField] private List<Button> buttonList  = new List<Button>();
    [FormerlySerializedAs("blackSmithObjectSO")] [SerializeField] private BlacksmithObjectSO blacksmithObjectSo;
    
    private int hammerHitNumber;
    private GameObject forgingObject;
    private Outline outline;
    private bool isBlackSmithObject = false;

    [Header("UI")] 
    [SerializeField] private Canvas productionCanvas;
    [SerializeField] private GameObject equipmentListPanel;
    [SerializeField] private GameObject forgingSliderPanel;
    [SerializeField] private Button equipmentListCloseButton;
    [SerializeField] private Button productionTableExitButton;
    

    #region IGetInteractable INTERFACE
    public void GetObject()
    {
        if (currentState != ProductionState.complete) return;
        Destroy(forgingObject);
        forgingList.RemoveAt(0);
        blacksmithObjectSo = null;
        currentState = ProductionState.select;
    }
    
    public void GiveObject()
    {
        if (currentState != ProductionState.select || !isBlackSmithObject) return;
       
        CoreGameSignals.OnPlayerCameraChange?.Invoke(true);
        CoreGameSignals.OnPlayerCanMove?.Invoke(false);
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.None);
        
        CoreUISignals.OnProductionTable_CanvasIsActive?.Invoke(true);
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive?.Invoke(true);
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive?.Invoke(false);
        
        OutlineDeactive();
        ButtonsCheck();
        
    }
    
    public GameObject GetPrefab()
    {
        return blacksmithObjectSo.prefab;
    }
    
    public BlacksmithObjectSO GetBlackSmithObjectSO()
    {
         
        BlacksmithObjectSO playerObjectSO = playerPickUpAndDropObject.GetBlackSmithObjectSO();
      
        foreach (Button button in buttonList)
        {
            if (button.GetComponent<EquipmentSelectButton>().GetMeltedToEquipmentSO().inputObject == playerObjectSO)
            {
                blacksmithObjectSo= playerObjectSO;
                isBlackSmithObject = true;
                return playerObjectSO;
            }
        }
        
        
        isBlackSmithObject = false;

        return blacksmithObjectSo;
    }

    public void OutlineActive()
    {
        outline.enabled = true;
    }
    
    public void OutlineDeactive()
    {
        outline.enabled = false;
    }

    #endregion

    #region ON ENABLE/DISABLE

    private void OnEnable()
    {
        CoreGameSignals.OnProductionTable_InstantiateObjectForForging += InstantiateObjectForForging;
        CoreGameSignals.ProductionTable_OnHammerHit += OnHammerHit;
        
        //UI
        CoreUISignals.OnProductionTable_CanvasIsActive += OnCanvasIsActive;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive += OnEquipmentListPanelIsActive;
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive += OnForgingSliderPanelIsActive;
    }
    
    
    
    private void InstantiateObjectForForging(BlacksmithObjectSO SO,Material material)
    {
        if (currentState != ProductionState.select) return;
        
        blacksmithObjectSo = SO;
        InstantiateObject();
        forgingObject.GetComponentInChildren<MeshRenderer>().material = material;
      
    }

    private void InstantiateObject()
    {
        if (forgingList.Count >=1)
        {
            Destroy( forgingList[0]);
            forgingList.RemoveAt(0);
        }
        GameObject prefab = blacksmithObjectSo.prefab;
        forgingObject = Instantiate(prefab, instantiatePoint.position , prefab.transform.rotation);
        forgingObject.transform.parent = instantiatePoint;
        forgingList.Add(forgingObject);
    }
    private void OnHammerHit()
    {
        hammerHitNumber++;
        if (hammerHitNumber>=1)
        {
            CoreGameSignals.OnPlayerCameraChange?.Invoke(false);
            CoreGameSignals.OnPlayerCanMove?.Invoke(true);
            CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
            CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive?.Invoke(false);
            hammerHitNumber = 0;
            InstantiateObject();
            currentState = ProductionState.complete;
        }
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
        
        CoreGameSignals.OnProductionTable_InstantiateObjectForForging -= InstantiateObjectForForging;
        CoreGameSignals.ProductionTable_OnHammerHit -= OnHammerHit;
        
        //UI
        CoreUISignals.OnProductionTable_CanvasIsActive -= OnCanvasIsActive;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive -= OnEquipmentListPanelIsActive;
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive -= OnForgingSliderPanelIsActive;
    }

    #endregion
    
    
    private void Awake()
    {
        outline = GetComponent<Outline>();
        
        equipmentListCloseButton.onClick.AddListener(EquipmentPanelOkButton);
        productionTableExitButton.onClick.AddListener(ProductionTableExit);
        currentState = ProductionState.select;
    }
    
    private void EquipmentPanelOkButton() // OK BUTTON CLICK
    {
        if (currentState != ProductionState.select) return;
        
        currentState = ProductionState.forge;
        CoreUISignals.OnProductionTable_EquipmentListPanelIsActive?.Invoke(false);
        CoreUISignals.OnProductionTable_ForgingSliderPanelIsActive?.Invoke(true);
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
        CoreGameSignals.PlayerPickUpAndDropObject_OnPickUpListRemove?.Invoke();
    }

    private void ProductionTableExit() // EXIT BUTTON CLICK
    {
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
        CoreGameSignals.OnPlayerCameraChange?.Invoke(false);
        CoreGameSignals.OnPlayerCanMove?.Invoke(true);
        
        Destroy(forgingObject);
        forgingList.Remove(forgingObject);
    }

    private void ButtonsCheck() // BUTTONS INTERACTABLE CHECK
    {
        BlacksmithObjectSO playerObjectSO = playerPickUpAndDropObject.GetBlackSmithObjectSO();
        
        foreach (Button button in buttonList)
        {
            if (button.GetComponent<EquipmentSelectButton>().GetMeltedToEquipmentSO().inputObject == playerObjectSO)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }
    
}
