using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowManager : MonoBehaviour
{
    [SerializeField]
    int PurchasableIndex;

    public float timeleft = 0f;
    int charging_cows = 0;
    Text buttontxt,bartxt;
    Transform bartransform;

    // Start is called before the first frame update
    void Start()
    {
        buttontxt = transform.GetChild(0).GetComponent<Text>();
        bartransform = transform.GetChild(1).GetChild(0).transform;
        bartxt = transform.GetChild(1).GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Workspace item = (Workspace)PlayerManager.Inst.purchases[PurchasableIndex];

        int available_cows = item.Available();
        float max_time = item.recharge_time;
        if (timeleft > 0)
        {
            timeleft -= Time.deltaTime; // decrease time
            bartxt.text = Mathf.Floor(timeleft * 100) + "%"; // update time counter

            if (timeleft <= 0) // if filled
            {
                bartxt.text = "All " + item.Name + "s refilled"; // set done text
                charging_cows--; // add cow back to available pool
                if (charging_cows > 0) // restart timer if needed
                    timeleft = max_time;
            }
        }


        buttontxt.text = item.actionphrase + "\n" +
            (available_cows - charging_cows) + "/" + available_cows; // update amount of available cows


        bartransform.localScale = new Vector3( (max_time - timeleft) / max_time, bartransform.localScale.y, bartransform.localScale.z); // charge bar fill
    }

    public void ProduceClick()
    {
        Workspace item = (Workspace)PlayerManager.Inst.purchases[PurchasableIndex];
        if (charging_cows < item.Available())
        {
            if(timeleft <= 0) // only reset bar is not actively charging
                timeleft = item.recharge_time; // start charge

            charging_cows++; // another cow needs recharging
            PlayerManager.Inst.money += item.value; // add value;
        }
    }
    // ???% complete
    // all cows refilled
}
