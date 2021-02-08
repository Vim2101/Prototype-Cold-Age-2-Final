using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vigilant : MonoBehaviour
{
    public GameObject preparing;
    public Transform prepDeck;
    public Transform recovDeck;
    public Transform markedDeck;
    public Transform Deck;
    public int startHandCount;
    public int maxHand;
    public int cardXScale;
    int checkForSorting;

    // Start is called before the first frame update
    void Start()
    {
        checkForSorting = Deck.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkForSorting != Deck.childCount)
            SortVigilantDeck();
    }

    public void AddInVigilantDeck(Transform card)
    {
        Debug.Log($"{Time.time} {card.GetComponent<Unit>().Name} added into Vigilant");
        card.SetParent(Deck);
        int i = Deck.childCount-1;
        card.transform.position = new Vector2(Deck.position.x + i * cardXScale, Deck.position.y);
    }

    public void SortVigilantDeck()
    {
        for (int i = 0; i < Deck.childCount; i++)
        {
            Deck.GetChild(i).transform.position = new Vector2(Deck.transform.position.x + i * cardXScale, Deck.transform.position.y);
        }
    }
            

    public void StartingHand()
    {
        for (int i = 0; i < startHandCount-1; i++) //considering that u pull one with preparing event
            preparing.GetComponent<Preparing>().DrawToVigilant();
    }

}
