using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckSystem : MonoBehaviour
{
    [SerializeField] private int deckSize = 10;
    [SerializeField] private int drawAmount = 1;
    [SerializeField] private int maxCardHand = 5;
    [SerializeField] private List<CardSO> deckCards;
    [SerializeField] private TextMeshProUGUI deckZoneText;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handZone;
    [SerializeField] private TurnSystem turnSystem;
    private int CardHolding = 0;

    private void Start()
    {
        ShuffleDeck();
        CardHolding = 0;
    }

    private void Update()
    {
        deckZoneText.text = $"Deck ({deckCards.Count})";

        if (deckSize == 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deckCards.Count; i++)
        {
            int randomCard = Random.Range(0,deckCards.Count);
            var original = deckCards[i];
            deckCards[i] = deckCards[randomCard];
            deckCards[randomCard] = original;
        }
    }

    public void DrawCard()
    {
        if (CardHolding >= maxCardHand || turnSystem.IsCardDrew()) { return; }

        for (int i = 0; i < drawAmount; i++)
        {
            Image cardImage = cardPrefab.GetComponentInChildren<Image>();
            cardImage = deckCards[deckCards.Count-1].cardImage;
            TextMeshProUGUI cardText = cardPrefab.GetComponentInChildren<TextMeshProUGUI>();
            cardText.text = deckCards[deckCards.Count-1].cardText;
            Instantiate(cardPrefab, handZone);
            deckCards.RemoveAt(deckCards.Count-1);
            CardHolding++;
            deckSize--;
            turnSystem.CardDrew();

            if (CardHolding >= maxCardHand) { return; }
        }
    }

    public void DecreaseCardHolding()
    {
        CardHolding--;
    }
}
