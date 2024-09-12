using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class  DealerManager : MonoBehaviour, IGetInteractable
{
    [Header("Spline")]
    [SerializeField] private SplineAnimate SplineAnimate;
    
    [Header("Wagon")]
    [SerializeField] private List<GameObject> WheeList;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private Animator horseAnimator;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI totalPriceText;
    [SerializeField] private Canvas canvas;
    [SerializeField] Button closeCanvasButton;
    
    private int totalPrice;
    private Outline outline;

    /// <summary>
    /// //////////////////////////////   ınteract ypap
    /// </summary>

    public void GetInteract()
    {
        SplineAnimate.Play();
    }

    public void OutlineActive()
    {
        outline.enabled = true;
    }

    public void OutlineDeactive()
    { 
        outline.enabled = false;
    }
    private void OnEnable()
    {
        CoreGameSignals.DealerManager_OnTotalPriceUpdate += TotalPriceUpdate;
    }

    private void OnDisable()
    {
        CoreGameSignals.DealerManager_OnTotalPriceUpdate -= TotalPriceUpdate;
    }

    private void Awake()
    {
        TotalPriceUpdate(0);
        closeCanvasButton.onClick.AddListener(CloseCanvas);
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        WagonMove(SplineAnimate.IsPlaying);
    }

    #region WAGON MOVEMENTw
    private void WagonMove(bool isMoving)
    {
        HorseAnimation(isMoving);
        RotateWheel(isMoving);
    }
    private void RotateWheel(bool isMoving)
    {
        if (!isMoving) return;
        foreach (var wheel in WheeList)
        {
            wheel.transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
        }
    }
    private void HorseAnimation(bool isWalking)
    {
        horseAnimator.SetBool("isWalk",isWalking);
    }

    #endregion
    
    public int GetTotalPrice()
    {
        return totalPrice;
    }

    private void TotalPriceUpdate(int addPrice)
    {
        totalPrice = totalPrice + addPrice;
        totalPriceText.text = " Total Price : "+totalPrice.ToString();
    }

    private void CloseCanvas()
    {
        canvas.enabled = false;
    }
}
