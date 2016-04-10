using UnityEngine;
using System.Collections;

public class CardFaceMan : MonoBehaviour {
    public Texture[] cardTextures = new Texture[GlobalVars.DECKAMOUNT];
    public Texture brokenCard;

    public Texture GetTextureForCard(int i,string s)
    {
        return this.cardTextures[i];
    }
}
