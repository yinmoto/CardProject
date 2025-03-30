using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnCountText;
    [SerializeField] private const float TURN_CHANGE_DELAY = 5.0f;
    private float delaytime;
    private bool isCardPlayed = false;
    private bool isCardDrew = false;
    private string playedCardText;
    
    private int turnNumber = 1;

    private void Update()
    {
        turnCountText.text = $"Turn {turnNumber}";

        if (isCardPlayed)
        {
            delaytime -= Time.deltaTime;
            if (delaytime <= 0f)
            {
                turnNumber++;
                isCardPlayed = false;
                isCardDrew = false;
            }
        }
        else
        {
            delaytime = TURN_CHANGE_DELAY;
        }
    }

    public void CardPlayed()
    {
        isCardPlayed = true;
    }

    public bool IsCardPlayed()
    {
        return isCardPlayed;
    }

    public void CardDrew()
    {
        isCardDrew = true;
    }

    public bool IsCardDrew()
    {
        return isCardDrew;
    }

    public void SetPlayedCardText(string playedCardText)
    {
        this.playedCardText = playedCardText;
    }

    public string GetPlayedCardText()
    {
        return playedCardText;
    }
}
