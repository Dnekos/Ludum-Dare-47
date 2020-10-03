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

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetChild(0).GetComponent<Text>().text =
                "Buy " + PlayerManager.Inst.purchases[i].Name + 
                "\n$" + PlayerManager.Inst.purchases[i].price.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Purchasable item = PlayerManager.Inst.purchases[i];

            if (item.price < PlayerManager.Inst.money || item.quantity > 0)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void BuyCow(int index)
    {
        Purchasable item = PlayerManager.Inst.purchases[index];
        if ((int)(item.price*100) <= (int)(PlayerManager.Inst.money*100))
        {
            PlayerManager.Inst.money -= (int)(item.price * 100) * 0.01f;
            item.price = item.price * item.growth_rate;
            item.quantity++;

            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = 
                "Buy " + item.Name + "\n$" + item.price.ToString("F2");
        }
    }

    public void GoToAcquisitions()
    {
        transform.parent.SetAsLastSibling();
    }
}
