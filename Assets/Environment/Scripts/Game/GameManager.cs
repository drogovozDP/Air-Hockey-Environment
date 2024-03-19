using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private AirhockerTrainer _airhockeyTrainer;

    public string gateTagBlue = "gateBlue";
    public string gateTagRed = "gateRed";
    public string playerTagBlue = "playerBlue";
    public string playerTagRed = "playerRed";
    public AirhockerAgent playerBlue;
    public AirhockerAgent playerRed;

    [Header ("Inference settings")]
    public bool inferenceMode = false;
    public TMP_Text scoreTextBlue;
    public TMP_Text scoreTextRed;

    private void Start()
    {
        _airhockeyTrainer = GetComponent<AirhockerTrainer>();
    }

    public void HitWasher(string playerTag)
    {
        if (!inferenceMode)
            _airhockeyTrainer.HitWasher(playerTag);
    }

    public void Goal(string goalGateTag)
    {
        if (goalGateTag == gateTagBlue)
        {
            playerRed.score++;
            scoreTextRed.text = "Red: " + playerRed.score;
                
        } 
        else if (goalGateTag == gateTagRed)
        {
            playerBlue.score++;
            scoreTextBlue.text = "Blue: " + playerBlue.score;
        }
        if (inferenceMode)
        {
            playerBlue.ResetGame();
            playerRed.ResetGame();
        }
        else
        {
            _airhockeyTrainer.Goal(goalGateTag);
        }
    }
}

