using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilkDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Inst.unlocked_milk)
            GetComponent<Text>().text = PlayerManager.Inst.money + " Cartons of Milk";

    }
}
