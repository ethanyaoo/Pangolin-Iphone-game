using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    //public GameObject lifeObj1, lifeObj2, lifeObj3;
    public Player player;
    public GameObject shield;

    private Text lifeDisplayText;

    public int healthCounter = 3;
    public int shieldCounter = 0;

    private void Start() 
    {
        lifeDisplayText = GetComponent<Text>();
    }

    public void takeDamage()
    {  
        print("TAKE DAMAGE");
        if (shieldCounter > 0)
        {
            shieldCounter--;
        }
        else if (healthCounter > 0)
        {
            healthCounter--;
        }
    }

    public void gainHealth()
    {
        if (healthCounter < 3)
        {
            healthCounter++;
        }
    }

    public void gainShield()
    {
        if (shieldCounter < 3)
        {
            shieldCounter++;
        }
    }

    private void FixedUpdate() 
    {
        switch(healthCounter)
        {
            case 3:
                lifeDisplayText.text = "Life: 3";
                break;
            case 2:
                lifeDisplayText.text = "Life: 2";
                break;
            case 1:
                lifeDisplayText.text = "Life: 1";
                break;
            case 0: 
                lifeDisplayText.text = "Life: 0";
                break;
        }

        switch(shieldCounter)
        {
            case 3:
                lifeDisplayText.text += "(+3)";
                break;
            case 2:
                lifeDisplayText.text += "(+2)";
                break;
            case 1:
                lifeDisplayText.text += "(+1)";
                break;
            case 0:
                shield.gameObject.SetActive(false);
                break;
        }

        if (shieldCounter > 0)
        {
            shield.gameObject.SetActive(true);
        }
    }
}
