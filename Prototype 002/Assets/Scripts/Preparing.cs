using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preparing : MonoBehaviour
{
    public GameObject Recovering;
    public Transform Deck;
    public Transform vigilantDeck;
    public Transform ShuffleDeck;
    public Transform recoveringDeck;
    public bool shuffled = false;
    Text text;
    public GameObject msgBoxPreparing;
    public Text msgBoxTextPreparing;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        ShufflePreparing();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountDeck();
    }

    void CountDeck() => text.text = $"{Deck.childCount}";

    public void SeeDeck()
    {
        if (Deck.childCount <= 0)
        {
            msgBoxTextPreparing.text = "Empty";
            msgBoxPreparing.SetActive(true);
            return;
        }
        msgBoxTextPreparing.text = "";
        for (int i = 0; i < Deck.childCount; i++)
        {
            msgBoxTextPreparing.text += $"{i+1} " + Deck.GetChild(i).GetComponent<Unit>().Name + "\n";
        }
        msgBoxPreparing.SetActive(true);
    }
    
    public void ShufflePreparing()
    {
        if (!shuffled)
        {
            shuffled = true;
            int count = Deck.childCount;
            for (int i = 0; i < count; i++)
                Deck.GetChild(Random.Range(0, Deck.childCount)).SetParent(ShuffleDeck);
            for (int i = 0; i < count; i++)
                ShuffleDeck.GetChild(Random.Range(0, ShuffleDeck.childCount)).SetParent(Deck);
            shuffled = false;
        }
    }
    public void DrawToVigilant()
    {
        if (vigilantDeck.parent.gameObject.GetComponent<Vigilant>().maxHand > vigilantDeck.childCount)
        {
            if (Deck.childCount <= 0)
            {
                Debug.Log($"{Time.time} Shuffled from Recovering to Preparing");
                int count = recoveringDeck.childCount;
                for (int i = 0; i < count; i++)
                    recoveringDeck.GetChild(0).SetParent(Deck);
                ShufflePreparing();
                Debug.Log($"{Time.time} {Deck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                Deck.GetChild(0).SetParent(vigilantDeck);
            }
            else
            {
                Debug.Log($"{Time.time} {Deck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                Deck.GetChild(0).SetParent(vigilantDeck);
            }
        }
    }
}
