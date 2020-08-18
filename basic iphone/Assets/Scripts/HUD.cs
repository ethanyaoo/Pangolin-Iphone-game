using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Text displayDistance;

    public void SetValues(float distanceTraveled)
    {
        //displayDistance.text = ((int)(distanceTraveled * 1f)).ToString();

        displayDistance.text = ((int)(distanceTraveled)).ToString();
    }
}
