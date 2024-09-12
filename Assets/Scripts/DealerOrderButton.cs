using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DealerOrderButton : MonoBehaviour
{
    [SerializeField] private StorageBoxManager storageBoxManager;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI pieceText;
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private Button increasePieceButton;
    [SerializeField] private Button decreasePieceButton;
    private int piece = 0;
    private int purchasePrice;


    private void OnEnable()
    {
        CoreGameSignals.DealerManager_OnReceiveOrder += OnReceiveOrder;
        CoreGameSignals.DealerOrderButton_OnPieceReset += OnPieceReset;
    }
    
    private void OnReceiveOrder()
    {
        storageBoxManager.ReceiveOrder(piece);
    }

    private void OnDisable()
    {
        CoreGameSignals.DealerManager_OnReceiveOrder -= OnReceiveOrder;
        CoreGameSignals.DealerOrderButton_OnPieceReset -= OnPieceReset;
    }

    private void Awake()
    {
        image.sprite =  storageBoxManager.GetBlackSmithObjectSO().objectSprite;
        nameText.text = storageBoxManager.GetBlackSmithObjectSO().objectName;
        purchasePrice = storageBoxManager.GetBlackSmithObjectSO().purchasePrice;
        purchaseText.text = storageBoxManager.GetBlackSmithObjectSO().purchasePrice.ToString();
        pieceText.text = piece.ToString();
    }
    private void Start()
    {
        increasePieceButton.onClick.AddListener(PieceIncrease);
        decreasePieceButton.onClick.AddListener(PieceDecrease);
    }

    private void PieceIncrease()
    {
        if (piece < storageBoxManager.GetNumberOfBarsUsed())
        {
            piece++;
            pieceText.text = piece.ToString();
            CoreGameSignals.DealerManager_OnTotalPriceUpdate?.Invoke(purchasePrice);
        }
    }
    private void PieceDecrease()
    {
        if (piece > 0)
        {
            piece--;
            pieceText.text = piece.ToString();
            CoreGameSignals.DealerManager_OnTotalPriceUpdate?.Invoke(-purchasePrice);
        }
    }
    
    private void OnPieceReset()
    {
        piece = 0;
        pieceText.text = piece.ToString();
        CoreGameSignals.DealerManager_OnTotalPriceUpdate?.Invoke(0);
    }
    
    
    
    
    
}
