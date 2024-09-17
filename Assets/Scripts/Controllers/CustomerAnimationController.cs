using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimationController : MonoBehaviour
{
    private CustomerAI customer;
    [SerializeField] private Animator animator;
    
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Interact = Animator.StringToHash("Interact");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Awake()
    {
        customer = GetComponent<CustomerAI>();
    }
    private void OnEnable()
    {
        CoreGameSignals.CustomerAI_OnWalkingAnimation += OnWalkingAnimation;
        CoreGameSignals.CustomerAI_OnIdleAnimation += OnIdleAnimation;
        
    }
    private void OnDisable()
    {
        CoreGameSignals.CustomerAI_OnWalkingAnimation -= OnWalkingAnimation;
        CoreGameSignals.CustomerAI_OnIdleAnimation -= OnIdleAnimation;
        
    }
    
    public void OnWalkingAnimation()
    {
        Debug.Log("Walking animation triggered");
        animator.SetTrigger(Walking);
    }
    public void OnIdleAnimation()
    {
        Debug.Log("idle animation triggered");
        animator.SetTrigger(Idle);
    }
    
    
    

}
