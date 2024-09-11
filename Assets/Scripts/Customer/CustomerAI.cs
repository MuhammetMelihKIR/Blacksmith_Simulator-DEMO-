using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class CustomerAI : MonoBehaviour,IGetInteractable
{
    [SerializeField] private PlayerPickUpAndDropObject playerPickUpAndDropObject;
    
    [Header("Customer Info")]
    private CustomerSO customerSO;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private string customerName;
    [SerializeField] private BlacksmithObjectSO blacksmithObjectSO;
    
    
    private NavMeshAgent agent;
    
    public void GetInteract()
    {
        if (playerPickUpAndDropObject.GetBlackSmithObjectSO() == blacksmithObjectSO )
        {
            blacksmithObjectSO = null;
            CoreGameSignals.OnPlayerPickUpAndDropObject_PickUpListRemove?.Invoke();
            print("oldu");
        }
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
