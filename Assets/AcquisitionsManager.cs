using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquisitionsManager : MonoBehaviour
{
    Text CowBuytext;
    // Start is called before the first frame update
    void Start()
    {
        CowBuytext = GameObject.Find("CowBuyTxt").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        CowBuytext.text = "Buy Cow\n$" + PlayerManager.Inst.cows.price.ToString("F2");
    }

    public void BuyCow()
    {
        Cow cows = PlayerManager.Inst.cows;
        if (cows.price < PlayerManager.Inst.money)
        {
            PlayerManager.Inst.money -= cows.price;
            cows.price = cows.price * cows.growth_rate;
            cows.quantity++;
        }
    }
}
