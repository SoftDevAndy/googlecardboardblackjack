using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    List<Card> allCards = new List<Card>();
    List<Card> playerCards = new List<Card>();
    List<Card> computerCards = new List<Card>();

    public int PlayerChipsCount = 1255;
    public int ComputerChipsCount = 1255;
    public int ThePotCount = 0;
    public GameObject blankCard;
    public GameObject playerCardPosition;
    public GameObject computerCardPosition;
    public GameObject pCardParent;
    public GameObject cCardParent;
    public Texture hiddenCardTexture;

    public GameObject player;
    public Animator computerAnimator;
    
    MainGui mainGui;
    AndyCamImpl andyCam;
    ChipStacks chipstacks;

    bool playerTurn = true;
    bool betting = true;
    bool waitReveal = false;
    bool quitting = false;
    bool justWait = false;

    int defaultBet = 35;

    void Start()
    {
        andyCam = GameObject.Find("GOD").GetComponent<AndyCamImpl>();
        mainGui = GameObject.Find("GOD").GetComponent<MainGui>();
        chipstacks = GameObject.Find("GOD").GetComponent<ChipStacks>();
        computerAnimator = GameObject.Find("RoboDealer").GetComponent<Animator>();
        ClearHands();
        SetupHand();
        playerBet(defaultBet);
    }

    void FixedUpdate()
    {
        mainGui.ShowChipCount();
        mainGui.ShowCardCount(waitReveal);
        mainGui.ShowPot(ThePotCount);

        //print("Betting: " + betting + " : " + andyCam.cameraState);

        if (!justWait)
        {
            if (!waitReveal)
            {
                if (playerTurn)
                {
                    if (betting)
                    {
                        mainGui.Prompt("Make a bet?\n(Nod) Yes? (Shake) No!\nPot Currently: " + ThePotCount);

                        if (currentState(AndyCamImpl.CamState.Nodded))
                        {
                            playerBet(defaultBet);
                        }

                        if (currentState(AndyCamImpl.CamState.HeadShook))
                        {
                            Debug.Log("Made it");

                            betting = false;
                        }
                    }
                    else
                    {
                        mainGui.Prompt("Do you want a card?\n(Nod) Yes? (Shake) No!\nPot Currently: " + ThePotCount);

                        if (currentState(AndyCamImpl.CamState.Nodded))
                        {
                            computerAnimator.Play("SecondCardPlayer");

                            Card temp = drawCard(true);
                            AddToHand(temp, true);
                            betting = true;
                        }

                        if (currentState(AndyCamImpl.CamState.HeadShook))
                        {
                            playerTurn = false;
                        }
                    }
                }
                else
                {
                    mainGui.Prompt("Do you want to stick?\n(Nod) Yes? (Shake) No!\nPot Currently: " + ThePotCount);

                    if (PlayerChipsCount <= defaultBet && !quitting)
                    {
                        quitting = true;

                        Debug.Log("Quitting");

                        computerAnimator.Play("DealerWins");

                        Vector3 theposition = new Vector3(24, 0.974f, 5.64f);
                        player.transform.position = theposition;

                        StartCoroutine(QuitGame());
                    }

                    if (ComputerChipsCount <= defaultBet && !quitting)
                    {
                        quitting = true;

                        Debug.Log("Quitting");

                        computerAnimator.Play("PlayerWins");

                        Vector3 theposition = new Vector3(12, 0.974f, 5.64f);
                        player.transform.position = theposition;

                        StartCoroutine(QuitGame());
                    }

                    if (getComputerCardCount(true) <= 16)
                    {
                        computerAnimator.Play("SecondCardDealer");

                        Card temp = drawCard(false);
                        AddToHand(temp, false);

                        playerTurn = true;
                        betting = true;
                    }

                    if (currentState(AndyCamImpl.CamState.Nodded))
                    {
                        if (getPlayerCardCount() == 21)
                        {
                            mainGui.Prompt("Player wins: " + ThePotCount);
                            computerAnimator.Play("PlayerWins");

                            ComputerChipsCount -= ThePotCount;
                            PlayerChipsCount += ThePotCount;
                        }
                        else if (getPlayerCardCount() > 21)
                        {
                            mainGui.Prompt("Computer wins: " + ThePotCount);
                            computerAnimator.Play("DealerWins");

                            ComputerChipsCount += ThePotCount;
                        }
                        else if (getComputerCardCount(true) > 21)
                        {
                            mainGui.Prompt("Player wins: " + ThePotCount);
                            computerAnimator.Play("PlayerWins");

                            ComputerChipsCount -= ThePotCount;
                            PlayerChipsCount += ThePotCount;
                        }
                        else if (getPlayerCardCount() < getComputerCardCount(true))
                        {
                            mainGui.Prompt("Computer wins: " + ThePotCount);
                            computerAnimator.Play("DealerWins");

                            ComputerChipsCount += ThePotCount;
                        }
                        else
                        {
                            mainGui.Prompt("Player wins: " + ThePotCount);
                            computerAnimator.Play("PlayerWins");

                            ComputerChipsCount -= ThePotCount;
                            PlayerChipsCount += ThePotCount;
                        }

                        SetupHand();
                    }

                    if (currentState(AndyCamImpl.CamState.HeadShook))
                    {
                        Debug.Log("Player Turn Over");

                        playerTurn = true;
                        betting = true;
                    }
                }
            }
        }
    }

    bool currentState(AndyCamImpl.CamState state)
    {
        if (andyCam.cameraState == state)
        {
            andyCam.cameraState = AndyCamImpl.CamState.None;
            return true;
        }
        else
            return false;
    }

    public void playerBet(int playerBet)
    {
        if (PlayerChipsCount > playerBet)
        {
            ThePotCount += playerBet;
            PlayerChipsCount -= playerBet;
            chipstacks.SetTableChips(PlayerChipsCount, ComputerChipsCount, ThePotCount);
        }
    }

    public int getPlayerCardCount()
    {
        int a = 0;
        int aceCount = 0;

        foreach (Card c in playerCards)
        {
            if (!c.getisAce())
                a += c.getValue();
            else
                aceCount++;
        }

        for (int i = 0; i < aceCount; i++)
        {
            if ((a + 10) <= 21)
                a += 10;
            else
                a += 1;
        }

        return a;
    }

    public int getComputerCardCount(bool flag)
    {
        int a = 0;
        int aceCount = 0;

        if (flag)
        {
            foreach (Card c in computerCards)
            {
                if (!c.getisAce())
                    a += c.getValue();
                else
                    aceCount++;
            }

            for (int i = 0; i < aceCount; i++)
            {
                if ((a + 10) <= 21)
                    a += 10;
                else
                    a += 1;
            }
        }
        else
        {
            a = computerCards[0].getValue();
        }

        return a;
    }

    void SetupHand()
    {
        StartCoroutine(WaitMessage());
    }

    IEnumerator WaitMessage()
    {
        justWait = true;

        StartCoroutine(RevealComputerCards());

        yield return new WaitForSeconds(2.0f);

        bool isPlayer = true;
        bool isComputer = false;

        ClearHands();

        Card temp = null;

        computerAnimator.Play("FirstDeal");

        temp = drawCard(isPlayer);
        AddToHand(temp, isPlayer);
        temp = drawCard(isPlayer);
        AddToHand(temp, isPlayer);

        temp = drawCard(isComputer);
        AddToHand(temp, isComputer);
        temp = drawCard(isComputer);
        AddToHand(temp, isComputer);

        chipstacks.SetTableChips(PlayerChipsCount, ComputerChipsCount, ThePotCount);

        justWait = false;
    }

    Card drawCard(bool isPlayer)
    {
        int pos = Random.Range(0, allCards.Count);

        Card card = allCards[pos];

        allCards.Remove(card);

        if(isPlayer)
            computerAnimator.Play("SecondCardPlayer");
        else
            computerAnimator.Play("SecondCardDealer");

        return card;
    }

    void AddToHand(Card card, bool isPlayer)
    {
        GameObject c = Instantiate(blankCard, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        
        if(isPlayer)
            c.gameObject.GetComponent<Renderer>().material.mainTexture = card.getCardTexture();

        if(!isPlayer)
        {
            if (computerCards.Count < 1)
                c.gameObject.GetComponent<Renderer>().material.mainTexture = card.getCardTexture();
            else
                c.gameObject.GetComponent<Renderer>().material.mainTexture = hiddenCardTexture;
        }

        if (isPlayer)
        {
            playerCards.Add(card);

            c.gameObject.transform.SetParent(pCardParent.transform);
            c.gameObject.transform.localPosition = new Vector3(playerCardPosition.transform.position.x, playerCardPosition.transform.position.y, playerCardPosition.transform.position.z);

            playerCardPosition.transform.position = new Vector3(playerCardPosition.transform.position.x + 0.1f, playerCardPosition.transform.position.y, playerCardPosition.transform.position.z);
        }
        else
        {
            computerCards.Add(card);

            c.gameObject.transform.SetParent(cCardParent.transform);
            c.gameObject.transform.localPosition = new Vector3(computerCardPosition.transform.position.x, computerCardPosition.transform.position.y, computerCardPosition.transform.position.z);

            computerCardPosition.transform.position = new Vector3(computerCardPosition.transform.position.x + 0.1f, computerCardPosition.transform.position.y, computerCardPosition.transform.position.z);
        }
    }

    void ClearHands()
    {
        int i = 0;

        foreach (Transform child in pCardParent.transform)
        {
            if(i != 0)
                GameObject.Destroy(child.gameObject);

            i++;
        }

        i = 0;

        foreach (Transform child in cCardParent.transform)
        {
            if (i != 0)
                GameObject.Destroy(child.gameObject);

            i++;
        }

        allCards = new List<Card>();
        playerCards = new List<Card>();
        computerCards = new List<Card>();

        playerCardPosition.transform.position = new Vector3(0, 0, 0);
        computerCardPosition.transform.position = new Vector3(0, 0, 0);

        GetComponent<ChipStacks>().SetTableChips(PlayerChipsCount, ComputerChipsCount, ThePotCount);

        resetDeck(GlobalVars.DECKAMOUNT);

        playerBet(defaultBet);
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(3.0f);

        Application.Quit();
    }

    void resetDeck(int deckAmount)
    {
        string cardName;
        bool isAce;
        bool isPicture;
        int val;
        Texture cardTexture = null;

        allCards = new List<Card>();

        for (int j = 0; j < deckAmount;j++)
        {
            for (int i = 0; i < GlobalVars.DECKSIZE; i++)
            {
                cardName = getName(i);
                isAce = getisAce(i);
                isPicture = getisPicture(i);

                if (isPicture)
                    val = 10;
                else
                    val = (i + 1) % 13;

                cardTexture = gameObject.GetComponent<CardFaceMan>().GetTextureForCard(i, cardName);

                allCards.Add(new Card(cardName, val, isAce, isPicture, cardTexture));
            }
        }
    }

    IEnumerator RevealComputerCards()
    {
        print(computerCards.Count);

        waitReveal = true;

        for (int i = 0; i < computerCards.Count; i++)
        {
            cCardParent.transform.GetChild((i + 1)).transform.gameObject.GetComponent<Renderer>().material.mainTexture = computerCards[i].getCardTexture();
        }

        yield return new WaitForSeconds(2.0f);
        
        ThePotCount = 0;

        waitReveal = false;
    }

    bool getisPicture(int i)
    {
        switch(i)
        {
            case 0:
            case 10:
            case 11:
            case 12:
                return true;

            case 13:
            case 23:
            case 24:
            case 25:
                return true;

            case 29:
            case 36:
            case 37:
            case 38:
                return true;

            case 39:
            case 49:
            case 50:
            case 51:
                return true;

            default:
                return false;
        }
    }

    string getName(int i)
    {
        string name = "";

        switch (i)
        {
            case 0:
            case 13:
            case 26:
            case 39:
                name = "Ace ";
                break;

            case 1:
            case 14:
            case 27:
            case 40:
                name = "Two ";
                break;

            case 2:
            case 15:
            case 28:
            case 41:
                name = "Three ";
                break;

            case 3:
            case 16:
            case 29:
            case 42:
                name = "Four ";
                break;

            case 4:
            case 17:
            case 30:
            case 43:
                name = "Five ";
                break;

            case 5:
            case 18:
            case 31:
            case 44:
                name = "Six ";
                break;

            case 6:
            case 19:
            case 32:
            case 45:
                name = "Seven ";
                break;

            case 7:
            case 20:
            case 33:
            case 46:
                name = "Eight ";
                break;
                
            case 8:
            case 21:
            case 34:
            case 47:
                name = "Nine ";
                break;

            case 9:
            case 22:
            case 35:
            case 48:
                name = "Ten ";
                break;

            case 10:
            case 23:
            case 36:
            case 49:
                name = "Jack ";
                break;

            case 11:
            case 24:
            case 37:
            case 50:
                name = "Queen ";
                break;

            case 12:
            case 25:
            case 38:
            case 51:
                name = "King ";
                break;
        }

        if(i == 0 || i / 13 == 0)
        {
            name += " of Spades";
        }
        if (i / 13 == 1)
        {
            name += " of Clubs";
        }
        if (i / 13 == 2)
        {
            name += " of Diamonds";
        }
        if (i / 13 == 3)
        {
            name += " of Hearts";
        }

        return name;
    }

    bool getisAce(int i)
    {
        switch(i)
        {
            case 0:
                return true;
            case 13:
                return true;
            case 26:
                return true;
            case 39:
                return true;
        }

        return false;
    }

}
