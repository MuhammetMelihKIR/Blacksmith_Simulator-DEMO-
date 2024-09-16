using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignboardUI : MonoBehaviour
{
    [SerializeField] private Camera canvasCamera;
    [SerializeField] private GameObject panel;
    [SerializeField] private InputField inputField;
    [SerializeField] private TextMeshProUGUI shopNameText;
    [SerializeField] private Button closeButton;
     
    [Header("textColor")]
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    private void Start()
    {
        shopNameText.text = "Default Shop Name";
        
        inputField.onValueChanged.AddListener(UpdateShopName);
        // Slayt değerleri değiştiğinde rengi güncelle
        redSlider.onValueChanged.AddListener(UpdateTextColor);
        greenSlider.onValueChanged.AddListener(UpdateTextColor);
        blueSlider.onValueChanged.AddListener(UpdateTextColor);
        
        closeButton.onClick.AddListener(CloseButton);
    }

    private void UpdateShopName(string newName)
    {
        shopNameText.text = newName;
    }
    private void UpdateTextColor(float value)
    {
        Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        shopNameText.color = newColor;
    }

    private void CloseButton()
    {
        panel.SetActive(false);
        canvasCamera.gameObject.SetActive(false);
        CoreGameSignals.GameState_OnStateChange?.Invoke(GameState.play);
        CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.Locked);
        CoreGameSignals.Player_OnPlayerCameraRotate?.Invoke(true);
        CoreGameSignals.OnPlayerCanMove?.Invoke(true);
    }
}
