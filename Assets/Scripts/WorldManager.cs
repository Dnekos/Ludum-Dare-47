using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    GameObject ProductionTab, UpgradeTab, ResearchTab, EndTab;
    [SerializeField]
    Text Countdown;
    
    float currenttime = 90;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Inst.money > 15)
            ProductionTab.SetActive(true);

        float minutes = Mathf.FloorToInt(currenttime / 60);
        float seconds = Mathf.FloorToInt(currenttime % 60);

        Countdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currenttime > 0)
            currenttime -= Time.deltaTime;
        else
        {
            EndTab.SetActive(true);
            EndTab.transform.SetAsLastSibling();
        }
    }
    public void EndTheWorld()
    {
        PlayerManager.Inst.Reset();
    }
}
