using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SojaExiles
{
    public class RegisterQueueManager : MonoBehaviour
    {
        public static RegisterQueueManager Instance { get; private set; }
        
        public Transform registerPosition; // The main register position
        public float spacingBetweenNPCs = 2f; // Distance between NPCs in the queue
        
        private Queue<animal_people_wolf_1> npcQueue = new Queue<animal_people_wolf_1>();
        private Dictionary<animal_people_wolf_1, Vector3> queuePositions = new Dictionary<animal_people_wolf_1, Vector3>();
        private List<animal_people_wolf_1> availableCustomers = new List<animal_people_wolf_1>();

        public UnityEvent<animal_people_wolf_1> onCustomerRegistered;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                FindAllCustomers();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void FindAllCustomers()
        {
            GameObject[] customerObjects = GameObject.FindGameObjectsWithTag("Customer");
            foreach (GameObject customerObj in customerObjects)
            {
                animal_people_wolf_1 customer = customerObj.GetComponent<animal_people_wolf_1>();
                if (customer != null)
                {
                    availableCustomers.Add(customer);
                    onCustomerRegistered?.Invoke(customer);
                }
            }
        }

        public void RegisterCustomer(animal_people_wolf_1 customer)
        {
            if (!availableCustomers.Contains(customer))
            {
                availableCustomers.Add(customer);
                onCustomerRegistered?.Invoke(customer);
            }
        }

        public Vector3 GetQueuePosition(animal_people_wolf_1 npc)
        {
            if (queuePositions.ContainsKey(npc))
            {
                return queuePositions[npc];
            }

            // Calculate new position based on queue length
            Vector3 position = registerPosition.position - (registerPosition.forward * spacingBetweenNPCs * npcQueue.Count);
            queuePositions[npc] = position;
            npcQueue.Enqueue(npc);
            
            return position;
        }

        public void RemoveFromQueue(animal_people_wolf_1 npc)
        {
            if (queuePositions.ContainsKey(npc))
            {
                queuePositions.Remove(npc);
                // Convert queue to list to remove specific NPC
                var tempList = new List<animal_people_wolf_1>(npcQueue);
                tempList.Remove(npc);
                npcQueue = new Queue<animal_people_wolf_1>(tempList);
                
                // Recalculate positions for remaining NPCs
                UpdateQueuePositions();
            }
        }

        private void UpdateQueuePositions()
        {
            int index = 0;
            queuePositions.Clear();
            
            foreach (var npc in npcQueue)
            {
                Vector3 newPosition = registerPosition.position - (registerPosition.forward * spacingBetweenNPCs * index);
                queuePositions[npc] = newPosition;
                npc.UpdateQueuePosition(newPosition);
                index++;
            }
        }

        public bool IsFirstInQueue(animal_people_wolf_1 npc)
        {
            return npcQueue.Count > 0 && npcQueue.Peek() == npc;
        }

        public void TriggerAllCustomersToRegister()
        {
            foreach (var customer in availableCustomers)
            {
                if (!npcQueue.Contains(customer))
                {
                    customer.StartMovingToRegister();
                }
            }
        }
    }
} 