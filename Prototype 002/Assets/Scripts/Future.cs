﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Future : MonoBehaviour
{
    public TriggerEventBroker trigger;
    public Transform Deck;
    public Transform answerBroker;
    public Transform ShuffleDeck;
    public Transform historyDeck;
    public Transform preparingDeck;
    bool shuffled = false;
    Text text;
    public GameObject msgBoxFuture;
    public Text msgBoxTextFuture;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        ShuffleFutureDeck();
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
            msgBoxTextFuture.text = "Empty";
            msgBoxFuture.SetActive(true);
            return;
        }
        msgBoxTextFuture.text = "";
        for (int i = 0; i < Deck.childCount; i++)
        {
            msgBoxTextFuture.text += " | " + $"{i + 1} " + Deck.GetChild(i).GetComponent<Event>().Name;
        }
        msgBoxFuture.SetActive(true);
    }

    public void ShuffleFutureDeck()
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

    public void DrawNextEventCard()
    {
        if (trigger.CheckAllTriggerEvents())
            return;
        if (Deck.childCount <= 0)
        {
            Debug.Log($"{Time.time} Shuffled from History to Future");
            int count = historyDeck.childCount;
            for (int i = 0; i < count; i++)
                historyDeck.GetChild(0).SetParent(Deck);
            ShuffleFutureDeck();
            Deck.GetChild(0).SetParent(answerBroker);
            answerBroker.GetChild(0).transform.position = new Vector2(answerBroker.position.x, answerBroker.position.y);
            preparingDeck.GetComponentInParent<Preparing>().DrawToVigilant(); //draw 1 new unit card for next turn

        }
        else
        {
            Deck.GetChild(0).SetParent(answerBroker);
            answerBroker.GetChild(0).transform.position = new Vector2(answerBroker.position.x, answerBroker.position.y);
            preparingDeck.GetComponentInParent<Preparing>().DrawToVigilant(); //draw 1 new unit card for next turn
        }      

    }
    public void DrawNextEventCardWithoutVigilant()
    {
        if (trigger.CheckAllTriggerEvents())
            return;
        if (Deck.childCount <= 0)
        {
            Debug.Log($"{Time.time} Shuffled from History to Future");
            int count = historyDeck.childCount;
            for (int i = 0; i < count; i++)
                historyDeck.GetChild(0).SetParent(Deck);
            ShuffleFutureDeck();
            Deck.GetChild(0).SetParent(answerBroker);
            answerBroker.GetChild(0).transform.position = new Vector2(answerBroker.position.x, answerBroker.position.y);

        }
        else
        {
            Deck.GetChild(0).SetParent(answerBroker);
            answerBroker.GetChild(0).transform.position = new Vector2(answerBroker.position.x, answerBroker.position.y);
        }

    }
}
