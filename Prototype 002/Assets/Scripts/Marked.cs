using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marked : MonoBehaviour
{
    public Transform Deck;
    public Transform vigilantDeck;
    public Transform recoveringDeck;
    public int cardXScale;
    int checkForSorting = 0;

    // Start is called before the first frame update
    void Start()
    {
        checkForSorting = Deck.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkForSorting != Deck.childCount)
            SortMarkedDeck();
    }

    public void AddInMarkedDeck(Transform card)
    {
        card.SetParent(Deck);
        int i = Deck.childCount-1;
        card.transform.position = new Vector2(Deck.position.x + i * cardXScale, Deck.position.y);
    }

    public void SortMarkedDeck()
    {
        for (int i = 0; i < Deck.childCount; i++)
            Deck.GetChild(i).transform.position = new Vector2(Deck.transform.position.x + i * cardXScale, Deck.transform.position.y);
    }
}
