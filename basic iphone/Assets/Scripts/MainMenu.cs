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
    private float goalScore, antScore, termiteScore, larvaScore;
    private float goalRange, antRange, termiteRange, larvaRange;
    private float timeDiffUpdate;
    public float antMultiplier, termiteMultiplier, larvaMultiplier, nearMissMult;
    public static Dictionary<string, float> pointsDict = new Dictionary<string, float>();
    private AudioListener audioListener;

    private void Start() 
    {
        hud.gameObject.SetActive(false);

        extraPointsText.gameObject.SetActive(false);

        audioListener = gameObject.GetComponent<AudioListener>();
    }

    public void gameStart()
    {
        audioListener.enabled = !audioListener.enabled;

        player.gameObject.SetActive(true);
        player.gameStart();
        gameObject.SetActive(false);
        hud.gameObject.SetActive(true);
    }

    public void endGame(float distanceTraveled, Dictionary<string, float> extraPointsDict)
    {
        audioListener.enabled = !audioListener.enabled;
        extraPointsText.gameObject.SetActive(true);
        pointsDict = extraPointsDict;

        scoreCount.text = ((int)(distanceTraveled * 1.0f)).ToString();

        endTotalScore = distanceTraveled * 1.0f;

        goalScore = endTotalScore + (float)(extraPointsDict["Ants"] * antMultiplier) 
                                    + (float)(extraPointsDict["Termites"] * termiteMultiplier)
                                        + (float)(extraPointsDict["Larva"] * larvaMultiplier)
                                            + (float)(pointsDict["Close"] * nearMissMult);

        larvaScore = endTotalScore + (float)(extraPointsDict["Ants"] * antMultiplier) 
                                    + (float)(extraPointsDict["Termites"] * termiteMultiplier)
                                        + (float)(extraPointsDict["Larva"] * larvaMultiplier);

        antScore = endTotalScore + (float)(extraPointsDict["Ants"] * antMultiplier);

        termiteScore = endTotalScore + (float)(extraPointsDict["Ants"] * antMultiplier)
                                        + (float)(extraPointsDict["Termites"] * termiteMultiplier);

        goalRange = goalScore - larvaScore;
        larvaRange = larvaScore - antScore;
        antRange = antScore - endTotalScore;
        termiteRange = termiteScore - antScore;

        hud.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    // Used as a timer to give the game ui effect of the text appearing one after another
    private void Update() 
    {
        if (endTotalScore > 0)
        {
            timeDiffUpdate = 200;

            if (endTotalScore > goalScore)
            {
                endTotalScore = goalScore;
            }
            else if (endTotalScore < antScore)
            {
                extraPointsText.text = "Ants Collected: " + ((int)(pointsDict["Ants"] * 1.0f)).ToString();

                if (endTotalScore < antScore)
                {
                    float addDiff = Mathf.Round(antRange / timeDiffUpdate);

                    endTotalScore += addDiff;
                }
            }
            else if (endTotalScore < termiteScore)
            {
                extraPointsText.text = "Ants Collected: " + ((int)(pointsDict["Ants"] * 1.0f)).ToString()
                    + "\n\n" + "Termites Collected: " + ((int)(pointsDict["Termites"] * 1.0f)).ToString();

                if (endTotalScore < termiteScore)
                {
                    float addDiff = Mathf.Round(termiteRange / timeDiffUpdate);

                    endTotalScore += addDiff;
                }
            }
            else if (endTotalScore < larvaScore)
            {
                extraPointsText.text = "Ants Collected: " + ((int)(pointsDict["Ants"] * 1.0f)).ToString()
                        + "\n\n" + "Termites Collected: " + ((int)(pointsDict["Termites"] * 1.0f)).ToString() 
                        + "\n\n" + "Larva Collected: " + ((int)(pointsDict["Larva"] * 1.0f)).ToString();

                if (endTotalScore < goalScore)
                {
                    float addDiff = Mathf.Round(larvaRange / timeDiffUpdate);

                    endTotalScore += addDiff;
                }
            }
            else if (endTotalScore <= goalScore)
            {
                float addDiff = Mathf.Round(goalRange / timeDiffUpdate);

                if (endTotalScore != goalScore) endTotalScore += addDiff;

                extraPointsText.text = "Ants Collected: " + ((int)(pointsDict["Ants"] * 1.0f)).ToString()
                        + "\n\n" + "Termites Collected: " + ((int)(pointsDict["Termites"] * 1.0f)).ToString() 
                        + "\n\n" + "Larva Collected: " + ((int)(pointsDict["Larva"] * 1.0f)).ToString()
                        + "\n\n" + "Close Dodge Points: " + ((int)(pointsDict["Close"] * 1.0f)).ToString();
            }

            scoreCount.text = ((int)(endTotalScore)).ToString();
        }
    }
}
