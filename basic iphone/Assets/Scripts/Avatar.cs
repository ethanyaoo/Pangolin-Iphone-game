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

    // Assignable Point Values for Powerups
    public float antPointScore = 100.0f;
    public float termitePointScore = 100.0f;
    public float larvaPointScore = 100.0f;
    public static Dictionary<string, float> extraPointsDict = new Dictionary<string, float>();

    private void Awake() 
    {
        player = transform.root.GetComponent<Player>();

    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Unbreakable" && deathCountdown < 0f)
        {
            if (healthCounter.healthCounter == 1 && healthCounter.shieldCounter == 0)
            {
                extraPointsDict.Clear();
                extraPointsDict.Add("Termites", termitesCount);
                extraPointsDict.Add("Ants", antsCount);
                extraPointsDict.Add("Larva", larvaCount);

                pangolinObject.gameObject.SetActive(false);

                Instantiate(playerEffect, transform.position, Quaternion.identity);

                deathCountdown = 0.5f;
            }
            else
            {
                healthCounter.takeDamage();
            }
        }
        else if (collider.tag == "Termites" || collider.tag == "Ants" || collider.tag == "Larva")
        {
            print("COllISION");
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
