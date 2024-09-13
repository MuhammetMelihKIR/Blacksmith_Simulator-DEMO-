using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class CustomerAI : MonoBehaviour,IGetInteractable
{
    [Header("Customer Info")]
    public PlayerPickUpAndDropObject player;
    private CustomerSO customerSO;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private string customerName;
    [SerializeField] private BlacksmithObjectSO blacksmithObjectSO;
    
    private NavMeshAgent agent;
    public void GetInteract()
    {
        if (blacksmithObjectSO == player.GetBlackSmithObjectSO())  
        {
            CoreGameSignals.PlayerPickUpAndDropObject_OnPickUpListRemove?.Invoke();
            CoreGameSignals.CustomerManager_OnProcessCustomerInQueue?.Invoke();
            CoreGameSignals.GoldManager_OnGoldUpdate?.Invoke(blacksmithObjectSO.salesPrice); 
            blacksmithObjectSO = null;
        }
    }

    public void OutlineActive()
    {
        
    }

    public void OutlineDeactive()
    {
        
    }

    public BlacksmithObjectSO GetBlacksmithObjectSO()
    {
        return blacksmithObjectSO;
    }
    
    public CustomerSO GetCustomerSO(CustomerSO newCustomerSO)
    {
        customerSO = newCustomerSO;
        return customerSO;
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void CustomerSOSetup()
    {
        customerPrefab = customerSO.customerPrefab;
        customerName = customerSO.customerName;
        
        GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
        customer.transform.parent = transform;
    }

    public void AgentGoToStartPoint(Transform startPoint)
    {
        agent.SetDestination(startPoint.position);
    }
    
    public void AgentGoToEndPoint(Transform endPoint)
    {
        agent.SetDestination(endPoint.position);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SaleTable"))
        {
            blacksmithObjectSO = customerSO.equipmentList.blacksmithObjectSOList[Random.Range(0, customerSO.equipmentList.blacksmithObjectSOList.Count)];
            CoreUISignals.CustomerManager_CustomerInfoUpdate?.Invoke(customerName,blacksmithObjectSO.objectName);
        }
    }
}
