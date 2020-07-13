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

	public float rotationRadius = 2.0f;

	private float posX, posY;
    // Start is called before the first frame update
    void Start()
    {
    }

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
			if (x2 > x1)// && (transform.eulerAngles.z < 140 || transform.eulerAngles.z > 220))
			{
				moveSpeed = 200f;
				transform.RotateAround(rotationCenter.position, -Vector3.left, moveSpeed * Time.deltaTime);
				// posX = rotationCenter.position.y + Mathf.Cos(transform.eulerAngles.z) * rotationRadius;
				// posY = rotationCenter.position.z + Mathf.Sin(transform.eulerAngles.z) * rotationRadius;
				// transform.position = new Vector3(0, posX, posY);
				// if (transform.eulerAngles.z > 270)
				// {
				// 	transform.position -= Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position += Vector3.left * Time.deltaTime * speedModifierX;
				// }
				// else if (transform.eulerAngles.z > 230)
				// {
				// 	transform.position -= Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position -= Vector3.left * Time.deltaTime * speedModifierX;
				// }
				// else if (transform.eulerAngles.z < 90 || transform.eulerAngles.z == 0)
				// {
				// 	transform.position += Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position += Vector3.left * Time.deltaTime * speedModifierX;
				// }
				// else if (transform.eulerAngles.z < 135)
				// {
				// 	transform.position += Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position -= Vector3.left * Time.deltaTime * speedModifierX;
				// }


				// if (transform.eulerAngles.z < 135 || transform.eulerAngles.z == 0)
				// {
				// 	transform.position += Vector3.up * Time.deltaTime * speedModifierY;
				// }
				// else if (transform.eulerAngles.z > 224)
				// {
				// 	transform.position -= Vector3.up * Time.deltaTime * speedModifierY;
				// }

				//transform.Rotate(0.0f, 0.0f, 3.0f * Time.deltaTime, Space.Self);
			}
			else if (x1 > x2)// && (transform.eulerAngles.z > 220 || transform.eulerAngles.z < 140))
			{
				moveSpeed = -200f;
				transform.RotateAround(rotationCenter.position, -Vector3.left, moveSpeed * Time.deltaTime);
				// posX = rotationCenter.position.y + Mathf.Cos(transform.eulerAngles.z) * rotationRadius;
				// posY = rotationCenter.position.z + Mathf.Sin(transform.eulerAngles.z) * rotationRadius;
				// transform.position = new Vector3(0, posX, posY);

				// if (transform.eulerAngles.z < 90)
				// {
				// 	transform.position -= Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position -= Vector3.left * Time.deltaTime * speedModifierX;
				// }
				// else if (transform.eulerAngles.z < 135)
				// {
				// 	transform.position -= Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position += Vector3.left * Time.deltaTime * speedModifierX;
				// }
				// else if (transform.eulerAngles.z > 270 || transform.eulerAngles.z == 0)
				// {
				// 	transform.position += Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position -= Vector3.left * Time.deltaTime * speedModifierX;
				// }
				// else if (transform.eulerAngles.z > 225)
				// {
				// 	transform.position += Vector3.up * Time.deltaTime * speedModifierY;
				// 	transform.position += Vector3.left * Time.deltaTime * speedModifierX;
				// }

				// if (transform.eulerAngles.z > 225 || transform.eulerAngles.z == 0)
				// {
				// 	transform.position += Vector3.up * Time.deltaTime * speedModifierY;
				// }
				// else if (transform.eulerAngles.z < 220)
				// {
				// 	transform.position -= Vector3.up * Time.deltaTime * speedModifierY;
				// }

				//transform.Rotate(0.0f, 0.0f, -3.0f * Time.deltaTime, Space.Self);
			}
        }
		else
		{
			moveSpeed = 0f;
		}
    }
}
