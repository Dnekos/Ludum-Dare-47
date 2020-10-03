using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowManager : MonoBehaviour
{
    public float timeleft = 0f;
    int charging_cows = 0;
    Text buttontxt,bartxt;
    Transform bartransform;

    // Start is called before the first frame update
    void Start()
    {
        buttontxt = GameObject.Find("CowText").GetComponent<Text>();
        bartransform = GameObject.Find("CowFillTop").transform;
        bartxt = GameObject.Find("CowFillText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int available_cows = PlayerManager.Inst.cows.Available();
        float max_time = PlayerManager.Inst.cows.recharge_time;
        if (timeleft > 0)
        {
            timeleft -= Time.deltaTime; // decrease time
            bartxt.text = Mathf.Floor(timeleft * 100) + "%"; // update time counter

            if (timeleft <= 0) // if filled
            {
                bartxt.text = "All cows refilled"; // set done text
                charging_cows--; // add cow back to available pool
                if (charging_cows > 0) // restart timer if needed
                    timeleft = max_time;
            }
        }


        buttontxt.text = "MILK COW\n" +
            (available_cows - charging_cows) + "/" + available_cows; // update amount of available cows


        bartransform.localScale = new Vector3( (max_time - timeleft) / max_time, bartransform.localScale.y, bartransform.localScale.z); // charge bar fill
    }

    public void CowClick()
    {
        if (charging_cows < PlayerManager.Inst.cows.Available())
        {
            if(timeleft <= 0) // only reset bar is not actively charging
                timeleft = PlayerManager.Inst.cows.recharge_time; // start charge

            charging_cows++; // another cow needs recharging
            PlayerManager.Inst.money += PlayerManager.Inst.cows.value; // add value;
        }
    }
    // ???% complete
    // all cows refilled
}
