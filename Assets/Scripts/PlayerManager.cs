using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Standard vars
    public Currency money;
    public Workspace[] purchases;
    public Worker worker;
    public Currency milk;
    public float[] automated_time_left;
    //public List<Upgrade> upgrades;

    // PRESTIGE vars
    public Currency tokens;
    public Research[] research;
    public bool unlocked_milk = false; // convert these to bitmap at some point
    public bool unlocked_tokens = false;
    public bool unlocked_Production = false;
    public bool unlocked_Upgrades = false;


    // manager
    public static PlayerManager Inst; // instance

    private void Awake()
    {
        if (Inst == null)
        {
            Debug.Log("creating Inst");
            Inst = this;

            money = new Currency(0,0);
            milk = new Currency(0, 0);
            tokens = new Currency(0, 0);
            research = new Research[10];
            for (int i = 0; i < research.Length; i++)
                research[i] = new Research(i);

            DontDestroyOnLoad(Inst);
        }
        else
            Destroy(this);

        Inst.SetUpWorld();
    }
    

    private void Update()
    {
        if (Inst.money > worker.price)
            unlocked_Production = true;
        
        if (purchases[2].quantity > 0)
            unlocked_milk = true;

        if (WorldManager.currenttime > 0)
            for (int i = 0; i < automated_time_left.Length;i++)
            {
                if (automated_time_left[i] < 0 && purchases[i].used > 0)
                {
                    automated_time_left[i] = purchases[i].recharge_time * worker.time_multiplier;
                    if (purchases[i].making_milk)
                        milk = milk + purchases[i].value * (float)purchases[i].used;
                    else
                        money = money + purchases[i].value * (float)purchases[i].used;
                }
                automated_time_left[i] -= Time.deltaTime;
            }
    }

    



    public void AddMoney(KeyValuePair<int, int> amount)
    {

    }

    public void Reset()
    {
        tokens = tokens + (money * 0.2f).Pow(0.8f);
        if (tokens > new Currency(0,0))
            unlocked_tokens = true;
        Inst.SetUpWorld();
        SceneManager.LoadScene(1);
    }

    void SetUpWorld()
    {
        //research 7-8, money & milk
        money = money * 0.02f * research[7].Rank;
        milk = milk * 0.02f * research[8].Rank;


        int[] quantities = new int[6];
        for (int i = 0; i < quantities.Length; i++)
        {
            if (purchases != null)
                quantities[i] = purchases[i].quantity;
            else
                quantities[i] = 0;
        }
        purchases = new Workspace[6];
        automated_time_left = new float[6];
        worker = new Worker();

        for (int i = 0; i < purchases.Length; i++) // researches 0-5
        {
            purchases[i] = new Workspace(i + 1);

            purchases[i].quantity += Mathf.Min((int)Mathf.Pow(2, research[i].Rank - 1), quantities[i]);
            GameObject.Find("AcquisitionsGrid").GetComponent<AcquisitionsManager>().UnlockUpgrades(purchases[i]);
        }
        
        // research 6
        worker.quantity = Mathf.CeilToInt(Mathf.Pow(2, research[6].Rank - 1) * 0.5f);
        GameObject.Find("AcquisitionsGrid").GetComponent<AcquisitionsManager>().UnlockUpgrades(worker);

        WorldManager.currenttime = 90 + 5 * research[9].Rank;

    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        if (upgrade.PurchasableIndex == -1)
        {
            GameObject.Find("Canvas").GetComponent<WorldManager>().Escape();
        }
        else if (upgrade.PurchasableIndex == 6)
        {
            switch (upgrade.Catagory)
            {
                case Up_Catagory.Time:
                    worker.time_multiplier *= upgrade.multiplier;
                    break;
                case Up_Catagory.Price:
                    worker.price *= upgrade.multiplier;
                    break;
            }
            worker.applied_upgrades++;
        }
        else
        {
            switch (upgrade.Catagory)
            {
                case Up_Catagory.Value:
                    purchases[upgrade.PurchasableIndex].value =
                        purchases[upgrade.PurchasableIndex].value * upgrade.multiplier;
                    break;
                case Up_Catagory.Time:
                    purchases[upgrade.PurchasableIndex].recharge_time *= upgrade.multiplier;
                    break;
                case Up_Catagory.Price:
                    purchases[upgrade.PurchasableIndex].price *= upgrade.multiplier;
                    break;
                case Up_Catagory.MilkCost:
                    purchases[upgrade.PurchasableIndex].milkcost =
                        (int)(purchases[upgrade.PurchasableIndex].milkcost * upgrade.multiplier);
                    break;
            }
            purchases[upgrade.PurchasableIndex].applied_upgrades++;
        }
    }
}
