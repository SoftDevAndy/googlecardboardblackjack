using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuDebug : MonoBehaviour {

    public Text debugLeft;
    public Text debugRight;

    float fadeTime = 1.5f;

    public void DebugMsg(string s)
    {
        StartCoroutine(ShowMsg(s));
    }

    IEnumerator ShowMsg(string s)
    {
        debugLeft.CrossFadeAlpha(0.0f, fadeTime, false);
        debugRight.CrossFadeAlpha(0.0f, fadeTime, false);

        yield return new WaitForSeconds(fadeTime);

        debugLeft.CrossFadeAlpha(1.0f, fadeTime, false);
        debugRight.CrossFadeAlpha(1.0f, fadeTime, false);

        debugLeft.text = s;
        debugRight.text = s;
    }

}
