using System;
using UnityEngine;
using UnityEngine.UI;

public class OvenClockSlider : MonoBehaviour
{
    public OvenManager OvenManager;
    
    [SerializeField] private Slider progressBar;
    [SerializeField] private float fillDuration = 5f; 
    private float elapsedTime = 0f;
    private bool isFilling = true;


    private void OnEnable()
    {
        CoreGameSignals.OvenManager_OnIsMelted += StartProgressBar;
    }

    private void OnDisable()
    {
        CoreGameSignals.OvenManager_OnIsMelted -= StartProgressBar;
    }

    private void Start()
    {
        progressBar.value = 0f;
    }

    private void Update()
    {
        if (!isFilling)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / fillDuration);
            
            if (progressBar.value >= 1f)
            {
                OnProgressComplete();
            }
        }
    }

    private void StartProgressBar(bool isfilling)
    {
        this.isFilling = isfilling;
        elapsedTime = 0f;
        progressBar.value = 0f;
    }

    private void OnProgressComplete()
    {
        OvenManager.CurrentState(OvenManagerState.melted);
    }
}
