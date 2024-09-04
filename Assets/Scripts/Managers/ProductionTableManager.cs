using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionTableManager : MonoBehaviour, ITakeable
{
    [SerializeField] private Material prefabMaterial;
    [SerializeField] private Transform instantiatePoint;
    [SerializeField]private List<GameObject> equipmentList = new List<GameObject>();

    [Header("UI")] 
    [SerializeField] private Canvas productionCanvas;
    [SerializeField] private Button button;
    
    private Outline outline;

    #region ITakeable INTERFACE

    public void GetObject()
    {
        
    }

    public void GiveObject()
    {
        
    }
    
    public GameObject GetPrefab()
    {
        return null;
    }
    
    public void OutlineActive()
    {
        outline.enabled = true;
    }
    
    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        return null;
    }

    #endregion

    private void OnEnable()
    {
        CoreGameSignals.Instance.OnInstantiateObjectProductionTable += InstantiateObject;
    }

    private void OnDisable()
    {
        CoreGameSignals.Instance.OnInstantiateObjectProductionTable -= InstantiateObject;
    }

    private void Awake()
    {
        outline = GetComponent<Outline>();
        button.onClick.AddListener(CloseCanvas);
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
    
    private void CloseCanvas()
    {
        productionCanvas.enabled = false;
    }
    
}
