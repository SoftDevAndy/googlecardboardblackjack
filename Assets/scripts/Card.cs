using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    int value = 0;
    string cardname = "";
    bool isAce = false;
    bool isPicture = false;
    Texture cardTexture;

    public Card(string name, int value, bool isAce,bool isPicture, Texture cardTexture)
    {
        this.value = value;
        this.cardname = name;
        this.isAce = isAce;
        this.isPicture = isPicture;
        this.cardTexture = cardTexture;
    }

    public Texture getCardTexture()
    {
        return this.cardTexture;
    }

    public string getName()
    {
        return this.cardname;
    }

    public int getValue()
    {
        return this.value;
    }

    public bool getisAce()
    {
        return this.isAce;
    }

    public bool getisPicture()
    {
        return this.isPicture;
    }
}
