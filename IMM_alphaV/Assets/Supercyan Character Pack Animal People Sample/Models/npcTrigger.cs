using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required for NavMeshAgent
using TMPro; // Required for TextMeshPro

namespace SojaExiles
{
    public class animal_people_wolf_1 : MonoBehaviour
    {
        public Transform RegisterLocation;
        public float proximityThreshold = 1f; // Distance threshold for when the NPC reaches the destination
        public string requestMessage = "Can I have a burger, please?";
        public TMP_Text uiText; // Reference to the TMP_Text component for TextMeshPro
        public Transform playerTransform; // Reference to the player's transform

        private NavMeshAgent npcAgent; // The NavMeshAgent component for movement
        private bool moveToTrigger = false; // Flag to control NPC movement

        void Start()
        {
            // Get the NavMeshAgent component attached to the NPC
            npcAgent = GetComponent<NavMeshAgent>();
            if (npcAgent == null)
            {
                Debug.LogError("NavMeshAgent component not found on NPC!");
            }

            // Ensure the UI text is initially empty or hidden
            if (uiText != null)
            {
                uiText.text = "";
            }
        }

        void Update()
        {
            if (moveToTrigger && npcAgent != null)
            {
                npcAgent.SetDestination(RegisterLocation.position);

                float distanceToDestination = Vector3.Distance(transform.position, RegisterLocation.position);
                if (distanceToDestination <= proximityThreshold)
                {
                    moveToTrigger = false;
                    npcAgent.ResetPath();
                    AskForBurger();
                }
            }

            // Make the NPC face the player
            FacePlayer();
        }

        // Public method to start the NPC movement, called by KitchenTriggerActivator
        public void StartMovingToRegister()
        {
            moveToTrigger = true;
        }

        void AskForBurger()
        {
            // Display the request message in the UI
            if (uiText != null)
            {
                uiText.text = requestMessage;
                Debug.Log("Request message displayed: " + requestMessage);
            }
        }

        void FacePlayer()
        {
            if (playerTransform != null)
            {
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                direction.y = 0; // Keep the NPC's rotation on the horizontal plane
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation
            }
            else
            {
                Debug.LogError("Player transform is not assigned!");
            }
        }
    }
}
