using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class StorageBoxManager : MonoBehaviour,ITakeable
{
    
    [SerializeField] private List<GameObject> bars = new List<GameObject>();
    [SerializeField] private BlacksmithObjectSO blacksmithObjectSo;
    private int numberOfBarsUsed;
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
        return blacksmithObjectSo.prefab;
    }
    
    public BlacksmithObjectSO GetBlackSmithObjectSO()
    {
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
  

    public BlacksmithObjectSO GetBlacksmithObjectSO()
    {
        return blacksmithObjectSo;
    }
    public int GetNumberOfBarsUsed()
    {
        numberOfBarsUsed= maxBars-currentIndex-1;
        return numberOfBarsUsed;
    }

    public int GetBarCount()
    {
        return bars.Count;
    }
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
    
    public void ReceiveOrder(int price)
    {
        for (int i = 0; i < price; i++)
        {
            IncreaseObject();
        }
    }
    
    
}
