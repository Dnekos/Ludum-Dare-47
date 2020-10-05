﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour
{

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            UpdateDescription(i);
    }

    public void BuyResearch(int index)
    {
        Currency cost = new Currency(Mathf.Pow(PlayerManager.Inst.research[index].Rank + 1, 2));
        if (cost <= PlayerManager.Inst.tokens)
        {
            PlayerManager.Inst.tokens = PlayerManager.Inst.tokens - cost;
            PlayerManager.Inst.research[index].Rank++;
            UpdateDescription(index);
        }
    }

    void UpdateDescription(int i)
    {
        Research research = PlayerManager.Inst.research[i];

        transform.GetChild(i).GetChild(0).GetComponent<Text>().text =
            research.Name + " Rank " + (research.Rank + 1) +
            ":\n" + Mathf.Pow(research.Rank + 1, 2) + "Crystals";

        switch (i) // description
        {
            case 0:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (int)Mathf.Pow(2, research.Rank) + " Cows come with you";
                break;
            case 1:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (int)Mathf.Pow(2, research.Rank) + " Goats come with you";
                break;
            case 2:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (int)Mathf.Pow(2, research.Rank) + " Creameries come with you";
                break;
            case 3:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (int)Mathf.Pow(2, research.Rank) + " Fromageries come with you";
                break;
            case 4:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (int)Mathf.Pow(2, research.Rank) + " Ice Cream Parlors come with you";
                break;
            case 5:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (int)Mathf.Pow(2, research.Rank) + " Yogurt Factorys come with you";
                break;
            case 6:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (int)Mathf.Pow(2, research.Rank) + " Farmhands come with you";
                break;
            case 7:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "Keep " + (2 * (research.Rank + 1)) + "% of Money";
                break;
            case 8:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "Keep " + (2 * (research.Rank + 1)) + "% of Milk";
                break;
            case 9:
                transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "Stay alive for " + (5 * (research.Rank + 1)) + " more seconds";
                break;
        }
    }
}
