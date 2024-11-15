using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AcceptFood : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            // get own foodScript type

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
        // check current gameobject for info script and get type if it exists
        // food type is pizza or hotdog

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
