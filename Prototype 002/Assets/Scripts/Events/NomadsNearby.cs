﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomadsNearby : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    // Start is called before the first frame update
    void Start()
    {
        eventScript = gameObject.GetComponent<Event>();
        broker = GameObject.FindWithTag("AnswerCard").GetComponent<AnswerBroker>();
        ButtonConfiguration();
        SetTextsOfButtons();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (eventScript.active)
        {
            AnswerAllUpdate();
        }
    }
    void ButtonConfiguration()
    {
        eventScript.button1.gameObject.SetActive(true);
        eventScript.button2.gameObject.SetActive(true);
        eventScript.button3.gameObject.SetActive(true);
        eventScript.button4.gameObject.SetActive(true);
    }
    void SetTextsOfButtons()
    {
        eventScript.button1Txt.text = "P>=2\nDraw one extra from preparing\nAdd Noise";
        eventScript.button2Txt.text = "P>=2\nIf two and more in future forsee next two events";
        eventScript.button3Txt.text = "P>=2\nChose 1 from preparing\nAdd Noise";
        eventScript.button4Txt.text = "Leave them alone.\nLose Noise.";
    }
    void AnswerAllUpdate()
    {
        if (broker.P >= 2)
        {
            eventScript.button1.interactable = true;
            eventScript.button2.interactable = true;
            eventScript.button3.interactable = true;
        }

        else
        {
            eventScript.button1.interactable = false;
            eventScript.button2.interactable = false;
            eventScript.button3.interactable = false;
        }
    }

    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();

        for (int i = 0; i < 2; i++)
            broker.preparing.GetComponent<Preparing>().DrawToVigilant();
        broker.noise++;
        broker.FinishingEventCardWithoutDraw(gameObject);
    }
    public void Answer2()
    {
        if (broker.FutureDeck.transform.childCount >1)
        {

            for (int i = 0; i < 2; i++)
                Debug.Log($"{Time.time} {i + 1} {broker.FutureDeck.transform.GetChild(i).GetComponent<Event>().Name}");
                
        }
        else
        {
            Debug.Log("Future is fuzzy....");
        }

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCardWithoutDraw(gameObject);
        broker.noise++;
        broker.selector.GetComponent<UnitSelector>().SortUnitCards(1);
    }
    public void Answer4()
    {
        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
        broker.noise--;
    }

}
