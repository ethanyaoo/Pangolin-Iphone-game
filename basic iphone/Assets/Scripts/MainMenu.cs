using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Player player;
    public Text scoreCount;
    public Text extraPointsText;
    public HUD hud;
    private float endTotalScore = -1;
    private float goalScore, antScore, termiteScore;
    public float antMultiplier, termiteMultiplier, larvaMultiplier;
    private float scoreTimer = -1.0f;
    public static Dictionary<string, float> pointsDict = new Dictionary<string, float>();

    private void Start() 
    {
        hud.gameObject.SetActive(false);

        extraPointsText.gameObject.SetActive(false);
    }

    public void gameStart()
    {
        player.gameObject.SetActive(true);
        player.gameStart();
        gameObject.SetActive(false);
        hud.gameObject.SetActive(true);
    }

    public void endGame(float distanceTraveled, Dictionary<string, float> extraPointsDict)
    {
        extraPointsText.text = "Ants Collected: " + ((int)(extraPointsDict["Ants"] * 1.0f)).ToString();
        extraPointsText.gameObject.SetActive(true);
        pointsDict = extraPointsDict;

        scoreTimer = 3.0f;

        scoreCount.text = ((int)(distanceTraveled * 1.0f)).ToString();
        endTotalScore = distanceTraveled * 1.0f;
        goalScore = endTotalScore + (float)(extraPointsDict["Ants"] * 50.0f) 
                                    + (float)(extraPointsDict["Termites"] * 50.0f)
                                        + (float)(extraPointsDict["Larva"] * 50.0f);

        antScore = endTotalScore + (float)(extraPointsDict["Ants"] * 50.0f);
        termiteScore = endTotalScore + (float)(extraPointsDict["Ants"] * 50.0f)
                                        + (float)(extraPointsDict["Termites"] * 50.0f);

        hud.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    // Used as a timer to give the game ui effect of the text appearing one after another
    private void FixedUpdate() 
    {
        if (endTotalScore > 0)
        {
            scoreTimer -= Time.deltaTime;

            if (endTotalScore > goalScore)
            {
                endTotalScore = goalScore;
                scoreTimer = -0.5f;
            }
            else if (endTotalScore < goalScore && scoreTimer <= 0.0f)
            {
                endTotalScore += 5.0f;
            }
            else if (scoreTimer <= 1.0f)
            {
                extraPointsText.text = "Ants Collected: " + ((int)(pointsDict["Ants"] * 1.0f)).ToString()
                        + "\n\n" + "Termites Collected: " + ((int)(pointsDict["Termites"] * 1.0f)).ToString() 
                        + "\n\n" + "Larva Collected: " + ((int)(pointsDict["Larva"] * 1.0f)).ToString();

                if (endTotalScore < goalScore)
                {
                    endTotalScore += 5.0f;
                }

            }
            else if (scoreTimer <= 2.0f)
            {
                extraPointsText.text = "Ants Collected: " + ((int)(pointsDict["Ants"] * 1.0f)).ToString()
                    + "\n\n" + "Termites Collected: " + ((int)(pointsDict["Termites"] * 1.0f)).ToString();

                if (endTotalScore < termiteScore)
                {
                    endTotalScore += 5.0f;
                }
            }
            else
            {
                if (endTotalScore < antScore)
                {
                    endTotalScore += 5.0f;
                }
            }

            scoreCount.text = ((int)(endTotalScore)).ToString();
        }
    }
}
