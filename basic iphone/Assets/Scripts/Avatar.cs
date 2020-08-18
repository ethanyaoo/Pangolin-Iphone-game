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
    public GameObject playerEffect, playerDamage;

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


    // Camera Movement

    public CameraShake cameraShake;
    public float shakeDuration, shakeMagnitude;

    // Audio Sources
    public AudioClip audioPlayerDie;
    public AudioClip audioPlayerHit;
    public AudioClip audioCollisionTermite;
    public AudioClip audioCollisionAnt;
    public AudioClip audioCollisionLarva;
    private AudioSource audioSrcPlayerDie;
    private AudioSource audioSrcPlayerHit;
    private AudioSource audioSrcCollisionTermite;
    private AudioSource audioSrcCollisionAnt;
    private AudioSource audioSrcCollisionLarva;

    private void Awake() 
    {
        player = transform.root.GetComponent<Player>();

        audioSrcPlayerDie = AddAudio(false, false, 0.3f);
        audioSrcPlayerHit = AddAudio(false, false, 0.3f);
        audioSrcCollisionTermite = AddAudio(false, false, 0.3f);
        audioSrcCollisionAnt = AddAudio(false, false, 0.3f);
        audioSrcCollisionLarva = AddAudio(false, false, 0.3f);

    }

    public AudioSource AddAudio (bool loop, bool playAwake, float volume)
    {
        AudioSource newAudio = gameObject.GetComponent<AudioSource>();

        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;

        return newAudio;
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

                StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
                Handheld.Vibrate();

                if (healthCounter.healthCounter == 1 && healthCounter.shieldCounter == 0)
                {
                    extraPointsDict.Add("Termites", termitesCount);
                    extraPointsDict.Add("Ants", antsCount);
                    extraPointsDict.Add("Larva", larvaCount);
                    extraPointsDict.Add("Close", closeScore);

                    audioSrcPlayerDie.clip = audioPlayerDie;
                    audioSrcPlayerDie.Play();

                    pangolinObject.gameObject.SetActive(false);

                    Instantiate(playerEffect, transform.position, Quaternion.identity);

                    deathCountdown = 0.5f;
                }
                else
                {
                    audioSrcPlayerHit.clip = audioPlayerHit;
                    audioSrcPlayerHit.Play();
                    Instantiate(playerDamage, transform.position, Quaternion.identity);
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
                    audioSrcCollisionTermite.clip = audioCollisionTermite;
                    audioSrcCollisionTermite.Play();
                    healthCounter.gainHealth();
                    termitesCount += 1.0f;
                    break;
                case "Ants":
                    audioSrcCollisionAnt.clip = audioCollisionAnt;
                    audioSrcCollisionAnt.Play();
                    antsCount += 1.0f;
                    break;
                case "Larva":
                    audioSrcCollisionLarva.clip = audioCollisionLarva;
                    audioSrcCollisionLarva.Play();
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

    public void Restart()
    {
        extraPointsDict.Clear();

        termitesCount = 0;
        antsCount = 0;
        larvaCount = 0;
        closeScore = 0;
    }
}
