using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Standard vars
    public float money;
    public Purchasable[] purchases;

    // PRESTIGE vars
    public int tokens;
    public List<int> research;

    // manager
    public static PlayerManager Inst; // instance
    

    // Start is called before the first frame update
    void Start()
    {
        purchases = new Purchasable[1];
        purchases[0] = new Cow(1);

        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(Inst);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
public class Prestige : MonoBehaviour
{
    public int tokens;
    public List<int> research;

    public Prestige()
    {
        research = new List<int>();
        tokens = 0;
    }
}
