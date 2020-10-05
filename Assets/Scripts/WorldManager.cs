using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    GameObject ProductionTab, UpgradeTab, ResearchTab, EndTab, endgame;
    [SerializeField]
    Text Countdown;

    bool game_end = false;

    public static float currenttime = 10;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currenttime);
        if (PlayerManager.Inst.unlocked_Production)
            ProductionTab.SetActive(true);
        if (PlayerManager.Inst.unlocked_Upgrades)
            UpgradeTab.SetActive(true);
        if (PlayerManager.Inst.tokens > 0)
        {
            ResearchTab.SetActive(true);

            if (currenttime != 0)
            {
                float minutes = Mathf.FloorToInt(currenttime / 60);
                float seconds = Mathf.FloorToInt(currenttime % 60);
                Countdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
                Countdown.text = "00:00";
        }
        else
        {
            Countdown.text = "";
        }

        if (currenttime < 10)
            Countdown.color = Color.red;
        if (currenttime > 0)
            currenttime -= Time.deltaTime;
        else
        {
            EndTab.SetActive(true);
            EndTab.transform.SetAsLastSibling();

            EndTab.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "You recieve "+(int)Mathf.Pow(PlayerManager.Inst.money * 0.2f, 0.8f)+" Crystalized Milk";
        }
    }
    public void EndTheWorld()
    {
        PlayerManager.Inst.Reset();
    }

    public void WinGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Escape()
    {
        currenttime = 0;

        endgame.SetActive(true);
        endgame.transform.SetAsLastSibling();
    }

    public void GoToTab()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.parent.SetAsLastSibling();
    }
}
