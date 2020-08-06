using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private Player player;
    public HealthCounter healthCounter;
    public GameObject pangolinObject;

    // Particle Systems
    public GameObject breakEffect;
    public GameObject playerEffect;

    public float deathCountdown = -1f;

    // Float values to calculate the number of ants, etc. picked up and the new score added 
    private float antsCount = 0.0f;
    private float termitesCount = 0.0f;
    private float larvaCount = 0.0f;
    private float closeScore = 0.0f;

    // Assignable Point Values for Powerups
    public static Dictionary<string, float> extraPointsDict = new Dictionary<string, float>();

    // Collision detectors

    private enum avatar {none, close, collide};
    private avatar currBehavior = 0;

    private void Awake() 
    {
        player = transform.root.GetComponent<Player>();

    }
    
    private void OnTriggerExit(Collider collider) 
    {
        currBehavior = 0;
    }

    private void OnTriggerEnter(Collider collider) 
    {
        currBehavior++;

        if (collider.tag == "Unbreakable" && deathCountdown < 0f)
        {
            if (currBehavior == avatar.close)
            {
                closeScore++;
            }
            else if (currBehavior == avatar.collide)
            {
                if (closeScore > 0) closeScore--;

                if (healthCounter.healthCounter == 1 && healthCounter.shieldCounter == 0)
                {
                    extraPointsDict.Clear();
                    extraPointsDict.Add("Termites", termitesCount);
                    extraPointsDict.Add("Ants", antsCount);
                    extraPointsDict.Add("Larva", larvaCount);
                    extraPointsDict.Add("Close", closeScore);

                    pangolinObject.gameObject.SetActive(false);

                    Instantiate(playerEffect, transform.position, Quaternion.identity);

                    deathCountdown = 0.5f;
                }
                else
                {
                    healthCounter.takeDamage();
                }
            }
        }
        else if ((collider.tag == "Termites" || collider.tag == "Ants" || collider.tag == "Larva") && currBehavior == avatar.collide)
        {
            collider.gameObject.SetActive(false);

            Instantiate(breakEffect, collider.transform.position, Quaternion.identity);

            player.powerUpUsage();

            switch(collider.tag)
            {
                case "Termites":
                    healthCounter.gainHealth();
                    termitesCount += 1.0f;
                    break;
                case "Ants":
                    antsCount += 1.0f;
                    break;
                case "Larva":
                    healthCounter.gainShield();
                    larvaCount += 1.0f;
                    break;
            }

            currBehavior = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (deathCountdown >= 0f)
        {
            deathCountdown -= Time.deltaTime;

            if (deathCountdown <= 0)
            {
                deathCountdown = -1f;
                player.Die(extraPointsDict);

            }
        }
    }
}
