using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    public GameObject shield;

    private Text lifeDisplayText;

    public int healthCounter = 3;
    public int shieldCounter = 0;

    private int resHC, resSC;

    private void Start() 
    {
        lifeDisplayText = GetComponent<Text>();

        resHC = healthCounter;
        resSC = shieldCounter;
    }

    public void Restart()
    {
        healthCounter = resHC;
        shieldCounter = resSC;
    }

    public void takeDamage()
    {  
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
