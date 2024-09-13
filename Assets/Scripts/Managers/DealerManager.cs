using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class DealerManager : MonoBehaviour, IGetInteractable
{
    [SerializeField] private DealerState dealerState;
    
    [Header("Spline")]
    [SerializeField] private SplineAnimate SplineAnimate;
    
    [Header("Wagon")]
    [SerializeField] private List<GameObject> WheeList;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private Animator horseAnimator;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI totalPriceText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button closeCanvasButton;
    [SerializeField] private Button orderConfirmButton;

    private int totalPrice;
    private Outline outline;
    
    private DealerState GetDealerState()
    {
        return dealerState;
    }
    private void SetDealerState(DealerState state)
    {
        dealerState = state;
    }

    #region IGetInteractable INTERFACE
    public void GetInteract()
    {
        if (GetDealerState()==DealerState.takeOrder)
        {
            CanvasActive();
        }
        else if (GetDealerState()==DealerState.receiveOrder)
        {
            CoreGameSignals.DealerManager_OnReceiveOrder?.Invoke();
            
            SetDealerState(DealerState.takeOrder);
            CoreGameSignals.DealerOrderButton_OnPieceReset?.Invoke();
        }
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

    #region  ONENABLE / ONDISABLE

    private void OnEnable()
    {
        CoreGameSignals.DealerManager_OnTotalPriceUpdate += TotalPriceUpdate;
    }

    private void OnDisable()
    {
        CoreGameSignals.DealerManager_OnTotalPriceUpdate -= TotalPriceUpdate;
    }

    #endregion
    

    private void Awake()
    {
        outline = GetComponent<Outline>();
        CanvasDeactive();
        
        SetDealerState(DealerState.takeOrder);
        
        TotalPriceUpdate(0);
        
        closeCanvasButton.onClick.AddListener(CloseButton);
        orderConfirmButton.onClick.AddListener(OrderConfirm);
    }

    private void Update()
    {
        if (SplineAnimate.NormalizedTime > 0.99f && GetDealerState() == DealerState.walk)
        {
            SetDealerState(DealerState.receiveOrder);
        }
        WagonMove(SplineAnimate.IsPlaying);
    }
    
    private int GetTotalPrice()
    {
        return totalPrice;
    }
    private void TotalPriceUpdate(int addPrice)
    {
        totalPrice = totalPrice + addPrice;
        totalPriceText.text = " Total Price : "+totalPrice.ToString();
    }
    
    private void OrderConfirm()
    {
        if (GetTotalPrice() > 0)
        {
            CoreGameSignals.GoldManager_OnGoldUpdate?.Invoke(-GetTotalPrice());
            SetDealerState(DealerState.walk);
            SplineAnimate.Restart( true );
            CanvasDeactive();
        }
    }

    #region WAGON MOVEMENT
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

    #region UI 

    private void CloseButton()
    {
        CanvasDeactive();
        CoreGameSignals.DealerOrderButton_OnPieceReset?.Invoke();
    }
    private void CanvasDeactive()
    {
        canvas.enabled = false;
        totalPrice = 0;
        TotalPriceUpdate(0);
        CoreGameSignals.OnPlayerCanMove?.Invoke(true);
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
        CoreGameSignals.Player_OnPlayerCameraRotate?.Invoke(true);
    }
    private void CanvasActive()
    {
        canvas.enabled = true;
        CoreGameSignals.OnPlayerCanMove?.Invoke(false);
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.None);
        CoreGameSignals.Player_OnPlayerCameraRotate?.Invoke(false);
    }
    

    #endregion
    
    
}
