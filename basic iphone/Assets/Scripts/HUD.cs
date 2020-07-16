using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Text displayDistance;

    public void SetValues(float timeTraveled)
    {
        //displayDistance.text = ((int)(distanceTraveled * 1f)).ToString();

        displayDistance.text = ((float)(timeTraveled * 1f)).ToString();
    }
}
