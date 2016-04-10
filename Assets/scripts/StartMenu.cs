using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMenu : MonoBehaviour {

    AndyCamImpl andyCam;
    MenuDebug menuDebug;

    bool on = false;
    bool firstWait = false;
    bool secondWait = false;

    void Start()
    {
        andyCam = GameObject.Find("GodOB").GetComponent<AndyCamImpl>();
        menuDebug = GameObject.Find("GodOB").GetComponent<MenuDebug>();

        WaitStart();
    }

    void FixedUpdate()
    {
        if (on)
        {
            if (firstWait && !secondWait)
            {
                if (currentState(AndyCamImpl.CamState.Nodded))
                {
                    StartCoroutine(Begin("Please shake your head from side to side (in disagreement)."));
                }
            }

            if (firstWait && secondWait)
            {
                if (currentState(AndyCamImpl.CamState.HeadShook))
                {
                    StartCoroutine(ChangeToGame());
                }
            }
        }
    }

    bool currentState(AndyCamImpl.CamState state)
    {
        if (andyCam.cameraState == state)
            return true;
        else
            return false;
    }

    void WaitStart()
    {
        StartCoroutine(Begin("Welcome to BadJack. \nPlease nod your head up and down (in agreement) to continue."));
    }

    IEnumerator Begin(string s)
    {
        if (on && firstWait)
            secondWait = true;


        yield return new WaitForSeconds(1.0f);
        
        firstWait = true;
        on = true;

        menuDebug.DebugMsg(s);
    }

    IEnumerator ChangeToGame()
    {
        StartCoroutine(Begin("Head gestures are working! Starting the game."));

        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene(1);
    }

}
