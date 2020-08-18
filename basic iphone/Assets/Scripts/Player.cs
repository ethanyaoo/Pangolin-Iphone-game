using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeSystem pipeSystem;
    private Pipe currentPipe;

    // Pipe/Player Vars
    public float velocity;
    private float distanceTraveled = 0f;
    private float deltaToRotation;
    private float systemRotation;

    private Transform world;
    private float worldRotation;
    private Vector3 avatarOrigin;
    private Quaternion avatarQuaternion;

    // Player Controls
    public Avatar avatar;
    public GameObject pangolinGameObject;
    public float rotationVelocity;
    private float avatarRotation;
    private Transform rotator;

    // HUD Control
    public HUD hud; 
    public MainMenu mainMenu;
    public HealthCounter healthCounter;
    private float timeTraveled, speedUpDistance, baseVelocity;

    public void gameStart()
    {        
        healthCounter.Restart();

        avatar.transform.position = avatarOrigin;
        avatar.transform.rotation = avatarQuaternion;

        velocity = baseVelocity;

        avatar.Restart();

        distanceTraveled = 0.0f;
        avatarRotation = 0.0f;
        systemRotation = 0.0f;
        worldRotation = 0.0f;
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();

        pangolinGameObject.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void Awake() 
    {
        world = pipeSystem.transform.parent;
        rotator = transform.GetChild(0);

        gameObject.SetActive(false);

        baseVelocity = velocity;

        avatarOrigin = avatar.transform.position;
        avatarQuaternion = avatar.transform.rotation;
    }

    private void Update() 
    {
        float delta = velocity * Time.deltaTime;

        distanceTraveled += delta;
        speedUpDistance += delta;

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

        //UpdateAvatarRotation();

        hud.SetValues(distanceTraveled);
    }

    private void FixedUpdate() 
    {
        if (speedUpDistance >= 250)
        {
            velocity += 1.0f;
            speedUpDistance = 0.0f;
        }
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
    }

    public void Die(Dictionary<string, float> extraPointsDict) 
    {
        mainMenu.endGame(distanceTraveled, extraPointsDict);
        gameObject.SetActive(false);
    }

    public void powerUpUsage()
    {
        timeTraveled -= 3.0f;
    }

    public float SpeedUpDistance
    {
        get
        {
            return speedUpDistance;
        }
    } 
}
