using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private Player player;

    public float deathCountdown = -1f;

    private void Awake() 
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collider) 
    {
        print("COLLISION");
        if (collider.tag == "Unbreakable" && deathCountdown < 0f)
        {
            deathCountdown = 1.0f;
        }
        else if (collider.tag == "PowerUp")
        {
            collider.gameObject.SetActive(false);

            player.powerUpUsage();
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
                print("Player Die");
                deathCountdown = -1f;
                //player.Die();
            }
        }
    }
}
