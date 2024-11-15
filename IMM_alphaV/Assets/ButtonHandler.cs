using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SojaExiles
{
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField]
        private string buttonId = ""; // Unique identifier for this button

        [SerializeField]
        private UnityEvent onButtonPressed; // Unity Event that will be triggered when button is pressed

        // Optional: Add a description to make it clear in the inspector what this button does
        [SerializeField]
        [TextArea(1, 3)]
        private string buttonDescription = "";

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform == transform)
                        {
                            ButtonPressed();
                        }
                    }
                }
                else
                {
                    Debug.LogError("Main camera not found. Please ensure there is a camera tagged as 'MainCamera'.");
                }
            }
        }

        public void ButtonPressed()
        {
            Debug.Log($"Button '{buttonId}' was pressed!");
            onButtonPressed?.Invoke();
        }

        // Getter for buttonId if needed from other scripts
        public string GetButtonId()
        {
            return buttonId;
        }
    }
}
