using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class Purchasable
{
    public string Name;
    int ID;

    public int quantity = 0; // how many cows
    public int used = 0; // how many are not working as a hand or being worked on
    public float price; // how much for a cow
    public float growth_rate; // how much price changes
    
    public int Available { get { return quantity - used; } }
}

public class Worker 
{
    
}

public class Workspace : Purchasable
{
    public int value; // how much they produce per click
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
        Debug.Log(connection);
        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        
        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Purchasables WHERE ID = " + ID;
        Debug.Log(query);
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            ID = int.Parse(reader[0].ToString());
            Name = reader[1].ToString();
            price = float.Parse(reader[2].ToString());
            growth_rate = float.Parse(reader[3].ToString());
            value = int.Parse(reader[4].ToString());
            actionphrase = reader[5].ToString();
            recharge_time = float.Parse(reader[6].ToString());
        }

        // Close connection
        dbcon.Close();
    }
}

/*public class Cow : Workspace
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
}*/