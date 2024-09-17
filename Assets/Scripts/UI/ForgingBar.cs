using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ForgingBar : MonoBehaviour
{ 
    [Header("UI Elements")]
    public Slider progressBar;
    public RectTransform greenZone; 
    public RectTransform movingLine;
    
    [Header("Settings")]
    public float movingSpeed; 
    private bool isMovingRight = true;
    private void OnEnable()
    {
        CoreGameSignals.PlayerController_Forge += OnHammerHit;
    }
    private void OnDisable()
    {
        CoreGameSignals.PlayerController_Forge -= OnHammerHit;
    }
    private void Update()
    {
        MoveLine();
    }
    private void OnHammerHit()
    {
        if (IsInGreenZone())
        {
            CoreGameSignals.ProductionTable_OnHammerHit?.Invoke();
            MoveGreenZone();
        }
    }
    private void MoveLine()
    {
        // Çizginin bar içindeki hareketi
        if (isMovingRight)
            movingLine.localPosition += Vector3.right * movingSpeed * Time.deltaTime;
        else
            movingLine.localPosition += Vector3.left * movingSpeed * Time.deltaTime;

        // Çizginin barın sınırlarına ulaşıp geri dönmesi
        if (movingLine.localPosition.x >= progressBar.GetComponent<RectTransform>().rect.width / 2)
            isMovingRight = false;
        else if (movingLine.localPosition.x <= -progressBar.GetComponent<RectTransform>().rect.width / 2)
            isMovingRight = true;
    }
    private bool IsInGreenZone()
    {
        return movingLine.localPosition.x > greenZone.localPosition.x - greenZone.rect.width / 2 &&
               movingLine.localPosition.x < greenZone.localPosition.x + greenZone.rect.width / 2;
    }
    private void MoveGreenZone()
    {
        float newX = Random.Range(
            -progressBar.GetComponent<RectTransform>().rect.width / 2 + greenZone.rect.width / 2, 
             progressBar.GetComponent<RectTransform>().rect.width / 2 - greenZone.rect.width / 2
        );
        greenZone.localPosition = new Vector3(newX, greenZone.localPosition.y, greenZone.localPosition.z);
        
    }
}
