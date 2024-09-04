using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgingBar : MonoBehaviour
{ 
    [Header("UI Elements")]
    public Slider progressBar;
    public RectTransform greenZone; 
    public RectTransform movingLine;
    
    [Header("Settings")]
    public float movingSpeed; 
    private bool isMovingRight = true;

    private void Update()
    {
        // Hareketli çizginin bar içinde sağa sola hareket etmesi
        MoveLine();

        // Space tuşuna basılma kontrolü
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Eğer hareketli çizgi yeşil bölgenin içindeyse
            if (IsInGreenZone())
            {
                Debug.Log("Correct Timing! Moving the green zone...");
                MoveGreenZone();
            }
            else
            {
                Debug.Log("Missed! Try again.");
            }
        }
    }

    private void MoveLine()
    {
        // Çizginin bar içinde sağa sola hareket etmesi
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
        // Hareketli çizginin yeşil bölgeyle çakışıp çakışmadığını kontrol et
        return movingLine.localPosition.x > greenZone.localPosition.x - greenZone.rect.width / 2 &&
               movingLine.localPosition.x < greenZone.localPosition.x + greenZone.rect.width / 2;
    }

    private void MoveGreenZone()
    {
        // Yeşil bölgenin yerini rastgele yeni bir konuma taşır
        float newX = Random.Range(
            -progressBar.GetComponent<RectTransform>().rect.width / 2 + greenZone.rect.width / 2, 
             progressBar.GetComponent<RectTransform>().rect.width / 2 - greenZone.rect.width / 2
        );
        greenZone.localPosition = new Vector3(newX, greenZone.localPosition.y, greenZone.localPosition.z);

        Debug.Log($"Green zone moved to {newX}");
    }
}
