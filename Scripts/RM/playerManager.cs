using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    //Give the Lost Boxes the 'Lost' tag, and the Police a 'Police' tag

    public float deliveryTime = 10.0f;
    float cooldow=2f;
    public int rewardVal = 20;
    public int coins = 0;
    public int lostObjects = 0;
    public int mission1 = 1;   //0 Not started | 1 started | 2 Completed
    public int mission2 = 0;
    public int mission3 = 0;
    public int mission4 = 0;
    public int noOfDeliveryLeft = 5;
    bool startSideQuest = false;
    bool idle = false;
    public Text coinText;
    public Text lostText;
    public Text returnLostText;
    public Text objectiveText;
    public Text timerText;
    bool canReturn = false;
    bool triggercooldown;
    public GameObject Spawns;

    private void Start()
    {
        returnLostText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (triggercooldown)
        {
            if (cooldow > 0)
            {
                cooldow -= Time.deltaTime;

            }
            else
            {
                objectiveText.text = "";
                triggercooldown = false;
            }
        }
        
        coinText.text = "Coins:" + coins;
        lostText.text = "Lost Objects: " + lostObjects;
        if (Input.GetKeyUp(KeyCode.E) && canReturn)
        {
            coins += lostObjects * rewardVal;
            lostObjects = 0;
        }
        if (mission2 == 2)
        {
            objectiveText.text = "DELIVER DA BOXES!!! ("+noOfDeliveryLeft+") Left!!!";
            deliveryTime -= Time.deltaTime;
            int seconds = (int)(deliveryTime % 60);
            int min = (int)(deliveryTime / 60);
            timerText.text = min + " " + seconds;
            if (seconds<0)
            {
                timerText.text = "";
                mission2 = 1;
                objectiveText.text = "You ran out of time! Go back to shop to retry!";
                Spawns.SetActive(false);
            }
            if (noOfDeliveryLeft == 0)
            {
                mission2 = 3;
                timerText.text = "";
                objectiveText.text = "MISSION PASSED RESPECT++";
                triggercooldown = true;
            }
        }
        else
        {
            
        }
    }

    private void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.gameObject.tag == "Lost")
        {
            Destroy(collidedObject.gameObject);
            lostObjects++;
            //coins += 20;
        }
        if (collidedObject.gameObject.tag == "Police")
        {
            canReturn = true;
            returnLostText.gameObject.SetActive(true);
        }
        if (collidedObject.gameObject.tag == "MazeEnd")
        {
            Destroy(collidedObject.gameObject);
            mission1 = 2;
            mission2 = 1;
            objectiveText.text = "Go to the shop to do some deliveries to earn money!";
        }
        if (mission2 == 1)
        {
            if (collidedObject.gameObject.tag == "Shop")
            {
                deliveryTime = 15f;
                mission2 = 2;
            }
        }
        if (mission2 == 2)
        {
            if (collidedObject.gameObject.tag == "Delivery")
            {
                noOfDeliveryLeft--;
                collidedObject.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider collidedObject)
    {
        returnLostText.gameObject.SetActive(false);
        canReturn = false;
    }
}
