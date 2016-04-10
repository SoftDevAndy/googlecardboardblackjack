using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainGui : MonoBehaviour {

    public bool showReal = false;

    public Text l_txtComputer, l_txtPlayer, l_txtPlayerCard, l_txtComputerCard,l_txtPrompt;
    public Text r_txtComputer, r_txtPlayer, r_txtPlayerCard, r_txtComputerCard,r_txtPrompt;

    public Text l_pot, r_pot;

    GameManager gameMan = null;

    void Start()
    {
        gameMan = GameObject.Find("GOD").GetComponent<GameManager>();
    }

    public void ShowPot(int pot)
    {
        l_pot.text = "" + pot;
        r_pot.text = "" + pot;
    }

    public void Prompt(string s)
    {
        l_txtPrompt.text = s;
        r_txtPrompt.text = s;
    }

    public void ShowChipCount()
    {
        l_txtComputer.text = "AI Chips: " + gameMan.ComputerChipsCount;
        r_txtComputer.text = "AI Chips: " + gameMan.ComputerChipsCount;
        l_txtPlayer.text = "Chips: " + gameMan.PlayerChipsCount;
        r_txtPlayer.text = "Chips: " + gameMan.PlayerChipsCount;
    }

    public void ShowCardCount(bool hideComp)
    {
        l_txtPlayerCard.text = "Cards: " + gameMan.getPlayerCardCount();
        r_txtPlayerCard.text = "Cards: " + gameMan.getPlayerCardCount();

        l_txtComputerCard.text = "Computer: " + gameMan.getComputerCardCount(hideComp);
        r_txtComputerCard.text = "Computer: " + gameMan.getComputerCardCount(hideComp);
    }
}
