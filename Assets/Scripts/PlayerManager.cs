using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Standard vars
    public float money;
    public Workspace[] purchases;
    public Worker worker;
    public float milk;
    //public List<Upgrade> upgrades;

    // PRESTIGE vars
    public int tokens;
    public List<int> research;
    public bool unlocked_milk = false; // convert these to bitmap at some point
    public bool unlocked_tokens = false;
    public bool unlocked_Production = false;
    public bool unlocked_Upgrades = false;


    // manager
    public static PlayerManager Inst; // instance
    

    // Start is called before the first frame update
    void Start()
    {
        if (Inst == null)
        {
            Debug.Log("creating Inst");
            Inst = this;
            DontDestroyOnLoad(Inst);
        }
        else
            Destroy(this);

        Inst.SetUpWorld();
    }

    private void Update()
    {
        if (Inst.money > purchases[5].price)
            unlocked_Production = true;
        
        if (purchases[2].quantity > 0)
            unlocked_milk = true;
    }

    public void Reset()
    {
        tokens += (int)Mathf.Pow(money * 0.2f, 0.8f);
        if (tokens > 0)
            unlocked_tokens = true;
        Inst.SetUpWorld();
        SceneManager.LoadScene(0);
    }

    void SetUpWorld()
    {
        money = 0;
        milk = 0;
        purchases = new Workspace[6];
        //upgrades = new List<Upgrade>();

        for (int i = 0; i < purchases.Length; i++)
            purchases[i] = new Workspace(i + 1);
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        if (upgrade.PurchasableIndex == 6)
            switch (upgrade.Catagory)
            {
                case Up_Catagory.Time:
                    worker.time_multiplier *= upgrade.multiplier;
                    break;
                case Up_Catagory.Price:
                    worker.price *= upgrade.multiplier;
                    break;
            }
        else
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
    }
}
