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

    private void Awake()
    {
        image.sprite =  storageBoxManager.GetBlackSmithObjectSO().objectSprite;
        nameText.text = storageBoxManager.GetBlackSmithObjectSO().objectName;
        purchaseText.text = storageBoxManager.GetBlackSmithObjectSO().purchasePrice.ToString();
        pieceText.text = piece.ToString();
    }

    private void Start()
    {
        increasePieceButton.onClick.AddListener(PieceIncrease);
        decreasePieceButton.onClick.AddListener(PieceDecrease);
    }


    public void PieceIncrease()
    {
        if (piece < storageBoxManager.GetNumberOfBarsUsed())
        {
            piece++;
            pieceText.text = piece.ToString();
        }
    }

    public void PieceDecrease()
    {
        if (piece > 0)
        {
            piece--;
            pieceText.text = piece.ToString();
        }
    }
}
