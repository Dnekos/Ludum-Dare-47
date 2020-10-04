using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class Purchasable
{
    public string Name;
    protected int ID;

    public int quantity = 0; // how many cows
    public int used = 0; // how many are not working as a hand or being worked on
    public float price; // how much for a cow
    public float growth_rate; // how much price changes

    public int first_upgrade_index;
    public int applied_upgrades = 0
        ;
    public int Available { get { return quantity - used; } }

    public bool Purchase()
    {
        if ((int)(price * 100) <= (int)(PlayerManager.Inst.money * 100))
        {
            PlayerManager.Inst.money -= (int)(price * 100) * 0.01f;
            price *= growth_rate;
            quantity++;
            return true;
        }
        return false;
    }
}

public class Worker : Purchasable
{
    public float time_multiplier;

    public Worker()
    {
        quantity = 0;
        used = 0;

        // Create database
        string connection = "URI=file:" + Application.dataPath + "/" + "LD47.db";
        //Debug.Log(connection);
        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Purchasables WHERE ID = " + ID;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            ID = int.Parse(reader[0].ToString());
            Name = reader[1].ToString();
            price = float.Parse(reader[2].ToString());
            growth_rate = float.Parse(reader[3].ToString());
            time_multiplier = float.Parse(reader[6].ToString());
            first_upgrade_index = int.Parse(reader[8].ToString());
            Debug.Log(first_upgrade_index);
        }

        // Close connection
        dbcon.Close();
    }
}

public class Workspace : Purchasable
{
    public int milkcost;

    public float value; // how much they produce per click
    public float recharge_time; // how long till can be clicked again

    public string actionphrase;

    public Workspace(int ID)
    {
        if (ID == 1)
            quantity = 1;
        else
            quantity = 0;
        used = 0;

        // Create database
        string connection = "URI=file:" + Application.dataPath + "/" + "LD47.db";
        //Debug.Log(connection);
        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        
        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Purchasables WHERE ID = " + ID;
        //Debug.Log(query);
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            ID = int.Parse(reader[0].ToString());
            Name = reader[1].ToString();
            price = float.Parse(reader[2].ToString());
            growth_rate = float.Parse(reader[3].ToString());
            value = float.Parse(reader[4].ToString());
            actionphrase = reader[5].ToString();
            recharge_time = float.Parse(reader[6].ToString());
            milkcost = int.Parse(reader[7].ToString());
            first_upgrade_index = int.Parse(reader[8].ToString());
        }

        // Close connection
        dbcon.Close();
    }
}

public enum Up_Catagory
{
    Value,
    Time,
    Price,
    MilkCost
}

public struct Upgrade
{
    public int ID;
    public string Name;
    public int PurchasableIndex;
    public Up_Catagory Catagory;
    public float multiplier;
    public int Unlock1, Unlock2;

    public Upgrade(int index)
    {
        // defaults
        ID = index;
        Name = "";
        PurchasableIndex = 0;
        Catagory = (Up_Catagory)0;
        multiplier = 0; 
        Unlock1 = 0;
        Unlock2 = 0;


        // Create database
        string connection = "URI=file:" + Application.dataPath + "/" + "LD47.db";
        //Debug.Log(connection);
        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Upgrades WHERE ID = " + ID;
        //Debug.Log(query);
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Name = reader[1].ToString();
            PurchasableIndex = int.Parse(reader[2].ToString());
            Catagory = (Up_Catagory) int.Parse(reader[3].ToString());
            multiplier = float.Parse(reader[4].ToString());
            Unlock1 = int.Parse(reader[5].ToString());
            Unlock2 = int.Parse(reader[6].ToString());
        }

        // Close connection
        dbcon.Close();
    }
}