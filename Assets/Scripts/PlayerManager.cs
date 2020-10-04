using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    // Standard vars
    public float money;
    public Purchasable[] purchases;
    public float milk;

    // PRESTIGE vars
    public int tokens;
    public List<int> research;


    // manager
    public static PlayerManager Inst; // instance
    

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        purchases = new Purchasable[6];
        for (int i = 0; i < purchases.Length; i++)
            purchases[i] = new Workspace(i + 1);

        if (Inst == null)
        {
            Debug.Log("creating Inst");
            Inst = this;
            DontDestroyOnLoad(Inst);
        }
        else
            Destroy(this);

        SetUpWorld();
    }

    public void Reset()
    {
        tokens += (int)Mathf.Pow(money * 0.2f, 0.8f);
        Inst.SetUpWorld();
        SceneManager.LoadScene(0);
    }

    void SetUpWorld()
    {
        money = 0;
        milk = 0;
        purchases = new Purchasable[6];
        for (int i = 0; i < purchases.Length; i++)
            purchases[i] = new Workspace(i + 1);
    }
}
