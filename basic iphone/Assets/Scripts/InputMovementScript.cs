using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovementScript : MonoBehaviour
{

	public float moveSpeed = 100f;
	private Touch touch;
	private float x1;
    private float x2;

	public Transform rotationCenter;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
        {
            x1 = Input.mousePosition.x;
        }

		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
		}

        if (touch.phase == TouchPhase.Moved)
        {
            x2 = Input.mousePosition.x;

			// EDIT FOR DEADZONE BETWEEN ANGLES 225 and 135
			if (x2 > x1 && (transform.position.y <= 0.4f || transform.position.z >= -0.40f)) // && (transform.eulerAngles.z < 140 || transform.eulerAngles.z > 220))
			{
				moveSpeed = 125f;
				transform.RotateAround(rotationCenter.position, -Vector3.left, moveSpeed * Time.deltaTime);
			}
			else if (x1 > x2 && (transform.position.y <= 0.4f || transform.position.z <= 0.40f)) // && (transform.eulerAngles.z > 220 || transform.eulerAngles.z < 140))
			{
				moveSpeed = -125f;
				transform.RotateAround(rotationCenter.position, -Vector3.left, moveSpeed * Time.deltaTime);
			}
        }
		else
		{
			moveSpeed = 0f;
		}
    }
}
