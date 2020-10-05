﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Upgrade heldUpgrade;
    [SerializeField]
    GameObject ButtonPrefab;
    public int index;

    float price;
    public float base_price;

    // Start is called before the first frame update
    void Start()
    {
        Purchasable item;
        heldUpgrade = new Upgrade(index);

        if (heldUpgrade.Catagory == Up_Catagory.WinGame)
            price = 2000000;
        else if (PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex].applied_upgrades == 0)
        {
            item = PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex];
            price = item.price * 2.5f;
            base_price = price;
        }
        else
        {
            item = PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex];
            price = base_price * Mathf.Pow(item.applied_upgrades, 2.5f);
        }

        switch (heldUpgrade.Catagory)
        {
            case Up_Catagory.Value:
                GetComponentInChildren<Text>().text = heldUpgrade.Name + ":\n" +
                    PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex].Name + "s worth " + 
                    (int)((heldUpgrade.multiplier - 1) *100) + "% more\n$"+price.ToString("F2");
                break;
            case Up_Catagory.Time:
                GetComponentInChildren<Text>().text = heldUpgrade.Name + ":\n" + 
                    PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex].Name + "s are " + 
                    (int)((1 - heldUpgrade.multiplier) * 100) + "% faster\n$" + price.ToString("F2"); // technically should be "NAME takes PERCENT less time"
                break;
            case Up_Catagory.Price:
                GetComponentInChildren<Text>().text = heldUpgrade.Name + ":\n" + 
                    PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex].Name + "s cost " + 
                    (int)((1 - heldUpgrade.multiplier) * 100) + "% less\n$" + price.ToString("F2");
                break;
            case Up_Catagory.MilkCost:
                GetComponentInChildren<Text>().text = heldUpgrade.Name + ":\n" + 
                    PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex].Name + " cost " + 
                    (int)((1 - heldUpgrade.multiplier) * 100) + "% less milk\n$" + price.ToString("F2");
                break;
            case Up_Catagory.WinGame:
                GetComponentInChildren<Text>().text = heldUpgrade.Name + ":\nEscape armageddon\n$" + price.ToString("F2");
                break;
        }
    }

    public void OnClick()
    {
        if (PlayerManager.Inst.money >= price)
        {
            PlayerManager.Inst.money -= price;

            PlayerManager.Inst.ApplyUpgrade(heldUpgrade);
            
            int num_upgrades = PlayerManager.Inst.purchases[heldUpgrade.PurchasableIndex].applied_upgrades;
            ButtonPrefab.GetComponent<UpgradeManager>().base_price = base_price;

            if (heldUpgrade.Unlock1 != 0)
            {
                ButtonPrefab.GetComponent<UpgradeManager>().index = heldUpgrade.Unlock1;
                Instantiate(ButtonPrefab, transform.parent);
            }
            if (heldUpgrade.Unlock2 != 0)
            {
                ButtonPrefab.GetComponent<UpgradeManager>().index = heldUpgrade.Unlock2;
                Instantiate(ButtonPrefab, transform.parent);
            }
            Destroy(gameObject);
        }
    }
}
