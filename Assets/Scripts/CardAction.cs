using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardAction : MonoBehaviour
{
    [SerializeField] private const float SHOW_CARD_DELAY = 2.0f;
    private Button myCard;
    private TurnSystem myTurnSystem;
    private DeckSystem myDeckSystem;
    private Canvas myCanvas;
    private GameObject popup;

    private void Start()
    {
        myTurnSystem = FindFirstObjectByType<TurnSystem>();
        myDeckSystem = FindFirstObjectByType<DeckSystem>();
        myCanvas = FindFirstObjectByType<Canvas>();
        myCard = GetComponent<Button>();


        myCard.onClick.AddListener(() =>
        {
            StartCoroutine(PlayCard());
        });
    }

    private IEnumerator PlayCard()
    {
        if (myTurnSystem.IsCardPlayed() && GetThisCardText() != myTurnSystem.GetPlayedCardText()) { yield break; }
        myTurnSystem.CardPlayed();

        myDeckSystem.DecreaseCardHolding();

        myTurnSystem.SetPlayedCardText(GetThisCardText());

        foreach (Transform child in myCanvas.transform)
        {
            if (child.name == "Popup")
            {
                popup = child.gameObject;
                popup.SetActive(true);
                TextMeshProUGUI popupText = popup.GetComponentInChildren<TextMeshProUGUI>();
                popupText.text = GetThisCardText(); 
            }
        }

        yield return new WaitForSeconds(SHOW_CARD_DELAY);

        foreach (Transform child in myCanvas.transform)
        {
            if (child.name == "DiscardZone")
            {
                GameObject card = Instantiate(gameObject, child);
                RectTransform cardRectTransform = card.GetComponent<RectTransform>();
                cardRectTransform.sizeDelta = new Vector2(200, 300);
                cardRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                cardRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            }
        }
        
        popup.SetActive(false);

        Destroy(gameObject);
    }

    private string GetThisCardText()
    {
        TextMeshProUGUI cardText = GetComponentInChildren<TextMeshProUGUI>();
        return cardText.text;
    }
}
