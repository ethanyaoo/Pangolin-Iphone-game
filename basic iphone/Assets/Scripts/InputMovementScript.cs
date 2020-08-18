using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovementScript : MonoBehaviour
{

	public float minSwipeDistanceX, minSwipeDistanceY;
	private bool currRolling;
	private float timeCount;

	private const float doubleClickTime = 0.2f;
	private float lastClickTime;
	private bool jump = false;


	private Touch initTouch = new Touch();
    private bool touching = false;

	public float movementSpeed, computerSpeed;

	[SerializeField] private Animator animatorController;

	private void Start() 
	{
		timeCount = -1;
		minSwipeDistanceX = 2.0f;
		minSwipeDistanceY = 300.0f;
	}

	private void OnTriggerEnter(Collider collider)
    {
		if (collider.tag == "Ants" || collider.tag == "Termites" || collider.tag == "Larva")
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


		foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)        //If finger touches the screen
            {
                if (touching == false)
                {
                    touching = true;
                    initTouch = touch;
                }
            }
            else if (touch.phase == TouchPhase.Moved)       //If finger moves while touching the screen
            {
				float deltaX = initTouch.position.x - touch.position.x;

				// Is supposed to detect the cases in which if you swipe left to go left but you reach the max height
				if (deltaX > 0 && (transform.position.y >= 0.4f && transform.position.z > 0.4f))
				{
					deltaX = 0;
				}
				else if (deltaX < 0 && (transform.position.y >= 0.4f && transform.position.z < -0.4f))
				{
					deltaX = 0;
				}

				if (deltaX > 0)
				{
					animatorController.SetBool("moveLeft", false);
					animatorController.SetBool("moveRight", true);
				}
				else if (deltaX < 0)
				{
					animatorController.SetBool("moveRight", false);
					animatorController.SetBool("moveLeft", true);
				}

                transform.RotateAround(Vector3.zero, transform.forward, deltaX * movementSpeed * Time.deltaTime);        //Rotates the player around the x axis
               

                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Ended)       //If finger releases the screen
            {
                initTouch = new Touch();
                touching = false;

				animatorController.SetBool("moveLeft", false);
				animatorController.SetBool("moveRight", false);
            }

			if (jump == true)
			{
				animatorController.SetBool("rollStart", true);
				animatorController.SetBool("rollJump", true);

				timeCount = 6.0f;
			}
        }

		if (Input.GetKey(KeyCode.A) && (transform.position.y <= 0.4f || transform.position.z <= 0.4f))
		{
			transform.RotateAround(Vector3.zero, transform.forward, computerSpeed * movementSpeed * Time.deltaTime);        //Rotates the player around the x axis
			
			animatorController.SetBool("moveLeft", false);
			animatorController.SetBool("moveRight", true);
		}
        else if (Input.GetKey(KeyCode.D) && (transform.position.y <= 0.4f || transform.position.z >= -0.4f))
		{
			transform.RotateAround(Vector3.zero, transform.forward, -computerSpeed * movementSpeed * Time.deltaTime);

			animatorController.SetBool("moveRight", false);
			animatorController.SetBool("moveLeft", true);
		}
    }
}
