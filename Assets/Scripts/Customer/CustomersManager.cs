using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomersManager : MonoBehaviour
 {
     [Header("CUSTOMERS INFO")]
     [SerializeField] private GameObject customerAI;

     [SerializeField] private CustormerListSO customerListSO;
     [SerializeField] private int maxCustomers;
     [SerializeField] private Queue<CustomerAI> customers = new Queue<CustomerAI>();

     [Header("MOVEMENT INFO")]
     
     [SerializeField] private List<Transform> queuePositions; 
     [SerializeField] private Transform endPoint;
     
     
     [Header("UI INFO")]
     [SerializeField] private TextMeshProUGUI customerNameText;
     [SerializeField] private TextMeshProUGUI orderText;
     [SerializeField] private Canvas worldCanvas;
     [SerializeField] private Transform cameraTransform;

     private void OnEnable()
     {
         CoreUISignals.CustomerManager_CustomerInfoUpdate += UpdateCustomerInfo;
         CoreGameSignals.OnCustomerManager_ProcessCustomerInQueue += ProcessCustomerInQueue;
     }

     private void OnDisable()
     {
         CoreUISignals.CustomerManager_CustomerInfoUpdate -= UpdateCustomerInfo;
         CoreGameSignals.OnCustomerManager_ProcessCustomerInQueue -= ProcessCustomerInQueue;
     }

     private void Awake()
     {
         for (int i = 0; i < maxCustomers; i++)
         {
             SpawnNewCustomerAtQueuePosition(i);
         }
     }
     
     private void Update()
     {
         CanvasEnable();
     }
     
     private void SpawnNewCustomerAtQueuePosition(int index)
     {
         GameObject customer = Instantiate(customerAI, queuePositions[index].position,  Quaternion.LookRotation(queuePositions[0].forward));
         customer.transform.parent = transform;
         customer.SetActive(false);

         // Random bir müşteri seç
         CustomerSO customerSO = customerListSO.customerSOList[Random.Range(0, customerListSO.customerSOList.Count)];
         customer.GetComponent<CustomerAI>().GetCustomerSO(customerSO);
         customer.GetComponent<CustomerAI>().CustomerSOSetup();

         // Müşteriyi sıraya ekle
         customers.Enqueue(customer.GetComponent<CustomerAI>());

         // Eğer sıra doluysa yeni müşteri inaktif
         if (index < queuePositions.Count)
         {
             customer.SetActive(true);
             customer.GetComponent<CustomerAI>().AgentGoToStartPoint(queuePositions[index]);
         }
     }
     
     // Sıradaki ilk müşteriyi hareket ettir, sıradakileri öne getir ve yeni müşteri ekle
     public void ProcessCustomerInQueue()
     {
         // İlk müşteriyi sıradan çıkar ve hedef noktaya gönder
         CustomerAI customer = customers.Dequeue();
         customer.AgentGoToEndPoint(endPoint);

         // Kalan müşteriler sırada bir adım öne gelsin
         UpdateCustomerQueuePositions();

         // Sıraya yeni müşteri ekle
         SpawnNewCustomerAtQueuePosition(queuePositions.Count - 1);
     }
     
     // Kalan müşterilerin sırasını güncelle
     private void UpdateCustomerQueuePositions()
     {
         CustomerAI[] remainingCustomers = customers.ToArray();
        
         for (int i = 0; i < remainingCustomers.Length; i++)
         {
             remainingCustomers[i].AgentGoToStartPoint(queuePositions[i]);
         }
     }
     
     public void UpdateCustomerInfo(string newName, string newOrder)
     {
         customerNameText.text = newName;
         orderText.text = newOrder;
     }
     
     
     // Karakter Canvas a bakmıyorsa canvas ı kapat
     private void CanvasEnable()
     {
         Vector3 directionToCamera = cameraTransform.position - worldCanvas.transform.position;
         float angle = Vector3.Angle(worldCanvas.transform.forward, directionToCamera);
        
         if (angle > 90)
         {
             worldCanvas.enabled = true; 
         }
         else
         {
             worldCanvas.enabled = false; 
         }
         
     }
    
    
     
     
 }
