using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AcceptFood : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            FoodType type = gameObject.GetComponent<FoodScript>().FoodType;

            if (other.gameObject.GetComponent<FoodScript>().FoodType != type)
            {
                text.text = "FUCK OFF";
            }
            else
            {
                text.text = "Food accepted";
                Debug.Log("Food accepted");
            }
        }

        gameObject.GetComponent<FoodScript>().FoodType = FoodType.pizza;
    }

    public bool acceptFood(FoodType type, GameObject heldFood) {
        if (gameObject.GetComponent<FoodScript>().FoodType != type)
        {
            text.text = "FUCK OFF";
            return false;
        }
        else
        {
            text.text = "Food accepted";
            Destroy(heldFood);
            return true;
        }
    }
}
