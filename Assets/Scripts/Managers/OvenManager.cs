using UnityEngine;
using UnityEngine.Serialization;

public class OvenManager : MonoBehaviour, ITakeable
{
    [SerializeField] private OvenManagerState currentState;
    [SerializeField] private MaterialToMeltedSO[] materialToMeltedSO;
    [SerializeField] private PlayerPickUpAndDropObject playerPickUpAndDropObject;
    [FormerlySerializedAs("blackSmithObjectSO")] [SerializeField] private BlacksmithObjectSO blacksmithObjectSo;
    
    [Header("UI")]
    [SerializeField] private GameObject ovenClockSlider;
    
    private Outline outline;
    
    #region ITakeable INTERFACE
    
    public void GetObject()
    {
        if (currentState != OvenManagerState.melted) return;
        CoreGameSignals.OnOvenManager_IsMelted?.Invoke(true);
        blacksmithObjectSo = null;
        OvenClockSliderSetActive(false);
        currentState = OvenManagerState.start;
    }

    public void GiveObject()
    {
        if (currentState != OvenManagerState.start) return;
        
        MeltToMaterial();
    }
    
    public GameObject GetPrefab()
    {
        if (currentState == OvenManagerState.melting) return null;
       
        return blacksmithObjectSo.prefab;
    }
    
    public void OutlineActive()
    {
       outline.enabled = true;
    }
    
    public BlacksmithObjectSO GetBlackSmithObjectSO()
    {
        if (currentState == OvenManagerState.melting) return null;
        
        foreach (MaterialToMeltedSO obj in materialToMeltedSO)
        {
            if (obj.inputMaterial==playerPickUpAndDropObject.GetBlackSmithObjectSO())
            {
                blacksmithObjectSo = playerPickUpAndDropObject.GetBlackSmithObjectSO();
                return blacksmithObjectSo;
            }
        }

        return blacksmithObjectSo;
    }

    #endregion

    
    #region ON ENABLE AND DISABLE

    private void OnEnable()
    {
        CoreGameSignals.OnOutline_Deactive += OnOutlineDeactive;
    }
    private void OnOutlineDeactive()
    {
        outline.enabled = false;
    }
    private void OnDisable()
    {
        CoreGameSignals.OnOutline_Deactive -= OnOutlineDeactive;
    }
    #endregion

    private void MeltToMaterial()
    {
        foreach (MaterialToMeltedSO obj in materialToMeltedSO)
        {
            if (obj.inputMaterial == playerPickUpAndDropObject.GetBlackSmithObjectSO())
            {
                blacksmithObjectSo = obj.outputMaterial;
                currentState = OvenManagerState.melting;
                OvenClockSliderSetActive(true);
                CoreGameSignals.OnOvenManager_IsMelted?.Invoke(false);
            }
        }
    }
    private void Awake()
    {
        outline = GetComponent<Outline>();
        currentState = OvenManagerState.start;
        OvenClockSliderSetActive(false);
    }
    
    public OvenManagerState CurrentState(OvenManagerState state)
    {
        currentState = state;
        return currentState;
    }
    
    private void OvenClockSliderSetActive(bool isActive)
    {
        ovenClockSlider.SetActive(isActive);
    }
    
}
