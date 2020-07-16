using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeSystem pipeSystem;
    public float velocity;
    private Pipe currentPipe;

    private float distanceTraveled = 0f;
    private float deltaToRotation;
    private float systemRotation;

    private Transform world;
    private float worldRotation;

    // Player Controls
    public float rotationVelocity;
    private float avatarRotation;
    private Transform rotator;
    public Transform playerObject;
    
    // HUD Control
    public HUD hud; 
    private float timeTraveled;
    
    private void Start() 
    {
        world = pipeSystem.transform.parent;
        rotator = transform.GetChild(0);
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();

        hud.SetValues(distanceTraveled);
    }

    private void Update() 
    {
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        timeTraveled += Time.deltaTime;
        systemRotation += delta * deltaToRotation;

        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = pipeSystem.SetupNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }

        pipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);

        UpdateAvatarRotation();

        hud.SetValues(timeTraveled);
    }

    // Rotates whole tunnel
    private void UpdateAvatarRotation()
    {
        avatarRotation += rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");

        if (avatarRotation < 0f)
        {
            avatarRotation += 360f;
        }
        else if (avatarRotation >= 360f)
        {
            avatarRotation -= 360f;
        }

        rotator.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;

        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);

        // Rotate player object like pipe system
        playerObject.transform.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    public void Die() 
    {
        gameObject.SetActive(false);
    }

    public void powerUpUsage()
    {
        timeTraveled -= 3.0f;
    }
}
