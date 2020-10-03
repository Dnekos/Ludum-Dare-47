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
        CowBuytext.text = "Buy Cow\n$" + PlayerManager.Inst.purchases[0].price.ToString("F2");
    }

    public void BuyCow(int index)
    {
        Purchasable item = PlayerManager.Inst.purchases[index];
        if ((int)(item.price*100) <= (int)(PlayerManager.Inst.money*100))
        {
            PlayerManager.Inst.money -= (int)(item.price * 100) * 0.01f;
            item.price = item.price * item.growth_rate;
            item.quantity++;
        }
    }
}
