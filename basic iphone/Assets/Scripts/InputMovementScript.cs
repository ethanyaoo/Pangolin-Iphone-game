using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovementScript : MonoBehaviour
{

	public float moveSpeed = 0.02f;
	public float minSwipeDistanceX, minSwipeDistanceY;
	private Touch touch;
	private float x1;
    private float x2;
	private bool currRolling;
	private float timeCount;

	private const float doubleClickTime = 0.2f;
	private float lastClickTime;
	private bool jump = false;

	public Transform rotationCenter;
	public GameObject breakEffect;
	[SerializeField] private Animator animatorController;
	//private int rollHash = Animator.StringToHash("Roll");

	private void Start() 
	{
		timeCount = -1;
		minSwipeDistanceX = 2.0f;
		minSwipeDistanceY = 300.0f;
	}

	private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Collisions" && currRolling)
        {
            //collider.gameObject.SetActive(false);

            //Instantiate(breakEffect, collider.transform.position, Quaternion.identity);
        }
		else if (collider.tag == "Ants" || collider.tag == "Termites" || collider.tag == "Larva")
		{
			animatorController.SetBool("rollStart", true);
			animatorController.SetBool("rollJump", false);

			timeCount = 6.0f;
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (timeCount > 0)
		{
			timeCount -= Time.deltaTime;

			if (timeCount <= 4) animatorController.SetBool("rollStart", false);
		}
		else if (timeCount <= 2)
		{
			animatorController.SetBool("rollJump", false);
			
			currRolling = false;
			jump = false;

			timeCount = -1;
		}

		if (Input.GetMouseButtonDown(0))
        {
            x1 = Input.mousePosition.x;

			float timeSinceLastClick = Time.time - lastClickTime;

			if (timeSinceLastClick <= doubleClickTime && jump != true)
			{
				jump = true;
			}
			else
			{
				jump = false;
			}

			lastClickTime = Time.time;
        }

		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
		}

        if (touch.phase == TouchPhase.Moved)
        {
            x2 = Input.mousePosition.x;
        }
    }

	private void FixedUpdate() 
	{
		float horizontalDistance = (new Vector3(x2, 0, 0) - new Vector3(x1, 0, 0)).magnitude;

		// EDIT FOR DEADZONE BETWEEN ANGLES 225 and 135
		if (x2 > x1 && horizontalDistance > minSwipeDistanceX && (transform.position.y <= 0.4f || transform.position.z >= -0.40f)) // && (transform.eulerAngles.z < 140 || transform.eulerAngles.z > 220))
		{
			if (moveSpeed < 0) moveSpeed *= -1;
			transform.RotateAround(rotationCenter.position, -Vector3.left, moveSpeed * horizontalDistance);
		}
		else if (x1 > x2 && horizontalDistance > minSwipeDistanceX && (transform.position.y <= 0.4f || transform.position.z <= 0.40f)) // && (transform.eulerAngles.z > 220 || transform.eulerAngles.z < 140))
		{
			if (moveSpeed > 0) moveSpeed *= -1;
			transform.RotateAround(rotationCenter.position, -Vector3.left, moveSpeed * horizontalDistance);
		}

		if (jump == true)
		{
			animatorController.SetBool("rollStart", true);
			animatorController.SetBool("rollJump", true);

			timeCount = 6.0f;
		}
	}
}
