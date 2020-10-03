using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable
{
    public string Name;

    public int quantity; // how many cows
    public int used; // how many are not working as a hand or being worked on
    public float price; // how much for a cow
    public float growth_rate; // how much price changes
    
    public int Available() { return quantity - used; }
}

public class Worker 
{
    
}

public class Workspace : Purchasable
{
    public int value; // how much they produce per click
    public float recharge_time; // how long till can be clicked again

    public string actionphrase;
}

public class Cow : Workspace
{
    public bool workable; // do cows count as workhands
    
    public Cow(int quan = 1)
    {
        Name = "Cow";
        actionphrase = "MILK COW";
        quantity = quan;
        workable = false;
        used = 0;

        price = 1; // default cost
        growth_rate = 1.15f;

        value = 1;
        recharge_time = 1;
    }
}