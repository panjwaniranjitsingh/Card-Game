using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    [SerializeField]private Card cardPrefab;
    [SerializeField] private Transform deckTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform pileOfCardsTransform;
    [SerializeField] private Sprite[] clubFaces;
    [SerializeField] private Sprite[] diamondFaces;
    [SerializeField] private Sprite[] heartFaces;
    [SerializeField] private Sprite[] spadeFaces;
    
    private static string[] suits = new string[] { "Club", "Diamond", "Heart", "Spade" };
    private static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    private List<string> deck;
    private List<Card> deckCards;
    private List<Card> playerCards;
    private List<Card> pileOfCards;
    private Player player;
    private UIManager uiManager;
    private void Start()
    {
        CreateAndShuffleDeck();
        CreateCards();
        pileOfCards = new List<Card>();
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();
        AssignCardsToPlayer();
    }

    private void CreateAndShuffleDeck()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        //foreach (string d in deck) 
        //{
        //    Debug.Log(d);
        //}
    }
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values) 
            {
                newDeck.Add(v + " " + s);
            }
        }
        return newDeck;
    }

    private void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while(n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    private void CreateCards()
    {
        deckCards = new List<Card>();
        List<string> emptyDeck = GenerateDeck();
        int i = 0;
        foreach(string card in deck)
        {
            Card newCard = Instantiate(cardPrefab,deckTransform);
            newCard.gameObject.name = card;
            newCard.cardValue = emptyDeck.IndexOf(card) % 13 + 1;
            newCard.SetFrontFace(GetCardFrontSprite(newCard));
            deckCards.Add(newCard);
            newCard.transform.localPosition += new Vector3(0, 0, -i * 0.05f);
            i++;
        }
    }

    private void AssignCardsToPlayer()
    {
        playerCards = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            Card card = FindRandomCard();
            ChangeCardParent(card, playerTransform);
        }
        uiManager.UpdateSumText(FindPlayerSum().ToString());
        CheckPlayerWin();
    }

    private Card FindRandomCard()
    {
        int random = Random.Range(0, deckCards.Count);
        Card card = deckCards[random];
        deckCards.Remove(card);
        return card;
    }

    private Sprite GetCardFrontSprite(Card card)
    {
        if (card.name.Contains("Club"))
        {
            return clubFaces[card.cardValue - 1];
        }
        else if (card.name.Contains("Diamond"))
        {
            return diamondFaces[card.cardValue - 1];
        }
        else if (card.name.Contains("Heart"))
        {
            return heartFaces[card.cardValue - 1];
        }
        else if (card.name.Contains("Spade"))
        {
            return spadeFaces[card.cardValue - 1];
        }
        else
        {
            Debug.LogError("Invalid Card Suit");
            return null;
        }
    }

    public int FindPlayerSum()
    {
        int sum = 0;
        foreach(Card card in playerCards)
        {
            sum += card.cardValue;
        }
        return sum;
    }
    public void ChangeCardParent(Card card,Transform newParent)
    {
        card.transform.SetParent(newParent);
        card.isFrontFace = true;
        if(newParent == playerTransform)
        { 
            card.transform.localPosition = Vector3.zero + new Vector3(playerCards.Count * 0.5f, 0, -playerCards.Count * 0.05f);
            playerCards.Add(card);
        }
        else if(newParent == pileOfCardsTransform)
        {
            card.transform.localPosition = Vector3.zero + new Vector3(0, 0, -pileOfCards.Count * 0.05f);
            pileOfCards.Add(card);
        }
    }
    public void HandlePlayerInput(Card selectedCard)
    {
        if (selectedCard.transform.parent == deckTransform)
        {
            ChangeCardParent(selectedCard,pileOfCardsTransform);
        }
        else if(selectedCard.transform.parent == pileOfCardsTransform && playerCards.Count < 3)
        {
            ChangeCardParent(selectedCard, playerTransform);
            pileOfCards.Remove(selectedCard);
        }
        else if (selectedCard.transform.parent == playerTransform)
        {
            ChangeCardParent(selectedCard,pileOfCardsTransform);
            playerCards.Remove(selectedCard);
            UpdatePlayerCardSprites();
        }
        uiManager.UpdateSumText(FindPlayerSum().ToString());
        CheckPlayerWin();
    }

    private void UpdatePlayerCardSprites()
    {
        for(int i = 0; i < playerCards.Count; i++)
        {
            playerCards[i].transform.localPosition = Vector3.zero + new Vector3(i * 0.5f, 0, -i * 0.05f);
        }
    }

    private void CheckPlayerWin()
    {
        if(FindPlayerSum() == 26)
        {
            Debug.Log("Player Wins");
            player.enabled = false;
            uiManager.ShowEndScreen();
        }
    }
}
