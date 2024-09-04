using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageBoxManager : MonoBehaviour,ITakeable
{
    
    [SerializeField] private List<GameObject> bars = new List<GameObject>();
    [SerializeField] private BlackSmithObjectSO blackSmithObjectSO;
    private int maxBars = 12;
    private int currentIndex;
    private Outline outline;
    

    #region ITakeable INTERFACE

    public void GetObject()
    {
        DecreaseObject();
    }
    
    public void GiveObject()
    {
        IncreaseObject();
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


    #region ON ENABLE AND DISABLE

    private void OnEnable()
    {
        CoreGameSignals.OnOutlineDeactive += OnOutlineDeactive;
    }
    
    private void OnOutlineDeactive()
    {
        outline.enabled = false;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnOutlineDeactive -= OnOutlineDeactive;
    }

    #endregion
    

    private void Awake()
    {
        outline = GetComponent<Outline>();
        currentIndex = maxBars - 1;
        
        for (int i = 0; i < maxBars; i++)
        {
            bars[i].SetActive(true);
        }
        
    }
    private void DecreaseObject()
    {
        if (currentIndex >= 0)
        {
            GameObject bar = bars[currentIndex];
            bar.SetActive(false);
            currentIndex--;
        } 
    }

    private void IncreaseObject()
    {
        if (currentIndex < maxBars - 1 && currentIndex < bars.Count - 1)
        {
            currentIndex++;
            GameObject bar = bars[currentIndex];
            bar.SetActive(true);
        }
    }
}
