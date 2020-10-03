using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    GameObject ProductionTab, UpgradeTab, ResearchTab;
    [SerializeField]
    Text Countdown;

    float lifespan = 90, currenttime = 90;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float minutes = Mathf.FloorToInt(currenttime / 60);
        float seconds = Mathf.FloorToInt(currenttime % 60);

        Countdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        currenttime -= Time.deltaTime;
        if (currenttime < 0)
        {
            PlayerManager.Inst.Reset();
        }
    }
}
