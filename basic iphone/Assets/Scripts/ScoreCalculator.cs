using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreCalculator : MonoBehaviour
{
	public Text highscore;
	public int Score;

    // Start is called before the first frame update
    void Start()
    {
        highscore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    	Score++;
        highscore.text = Score.ToString();
    }
}
