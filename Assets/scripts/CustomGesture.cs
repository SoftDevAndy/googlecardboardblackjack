using UnityEngine;
using System.Collections;

public class CustomGesture : MonoBehaviour {

    public GameObject cammy;
    public CameraControl cameraControl;

    bool down = false;
    bool up = false;
    bool left = false;
    bool right = false;
    bool nodCheck = false;
    bool shakeHead = false;

    float nodeHeadTuneTime = 0.6f;
    float shakeHeadTuneTime = 0.6f;

    MenuDebug menudebug;

    void Start()
    {
        menudebug = GameObject.Find("GodOB").GetComponent<MenuDebug>();
    }

    void Update()
    {
        if (cameraControl.camstateX == CamStateX.Down && !nodCheck)
        {
            print("Try NodHead DownUpDownUp");

            nodCheck = true;
            StartCoroutine(NodHead_DownUpDownUp());
        }
        else if(cameraControl.camstateX == CamStateX.Up && !nodCheck)
        {
            print("Try NodHead UpDownUpDown");

            nodCheck = true;
            StartCoroutine(NodHead_UpDownUpDown());
        }

        if (cameraControl.camstateY == CamStateY.Right && !shakeHead)
        {
            print("Try ShakeHead_LeftRightLeftRight");

            shakeHead = true;
            StartCoroutine(ShakeHead_LeftRightLeftRight());
        }
        else if (cameraControl.camstateY == CamStateY.Left && !shakeHead)
        {
            print("Try ShakeHead_RightLeftRightLeft");

            shakeHead = true;
            StartCoroutine(ShakeHead_RightLeftRightLeft());
        }
    }

    IEnumerator NodHead_DownUpDownUp()
    {
        yield return new WaitForSeconds(nodeHeadTuneTime);

        if (cameraControl.camstateX != CamStateX.Down)
        {
            up = true; 

            yield return new WaitForSeconds(nodeHeadTuneTime);

            if (cameraControl.camstateX == CamStateX.Down && up)
            {
                down = true;
            }

            yield return new WaitForSeconds(nodeHeadTuneTime);

            if (cameraControl.camstateX != CamStateX.Down && down)
            {
                menudebug.DebugMsg("NodHead_DownUpDownUp()");

                print("NodHead_DownUpDownUp()");
            }
        }

        resetNod();
    }

    IEnumerator NodHead_UpDownUpDown()
    {
        yield return new WaitForSeconds(nodeHeadTuneTime);

        if (cameraControl.camstateX != CamStateX.Up)
        {
            down = true;

            yield return new WaitForSeconds(nodeHeadTuneTime);

            if (cameraControl.camstateX == CamStateX.Up && down)
            {
                up = true;
            }

            yield return new WaitForSeconds(nodeHeadTuneTime);

            if (cameraControl.camstateX != CamStateX.Up && up)
            {
                menudebug.DebugMsg("NodHead_UpDownUpDown()");

                print("NodHead_UpDownUpDown()");
            }
        }

        resetNod();
    }

    IEnumerator ShakeHead_LeftRightLeftRight()
    {
        yield return new WaitForSeconds(shakeHeadTuneTime);

        if (cameraControl.camstateY != CamStateY.Right)
        {
            left = true;
            
            yield return new WaitForSeconds(shakeHeadTuneTime);

            if (cameraControl.camstateY == CamStateY.Right && left)
            {
                right = true;
            }

            yield return new WaitForSeconds(shakeHeadTuneTime);

            if (cameraControl.camstateY != CamStateY.Right && right)
            {
                menudebug.DebugMsg("ShakeHead_LeftRightLeftRight()");

                print("ShakeHead_LeftRightLeftRight()");
            }
        }

        resetShake();
    }

    IEnumerator ShakeHead_RightLeftRightLeft()
    {
        yield return new WaitForSeconds(shakeHeadTuneTime);

        if (cameraControl.camstateY != CamStateY.Left)
        {
            right = true;

            yield return new WaitForSeconds(shakeHeadTuneTime);

            if (cameraControl.camstateY == CamStateY.Left && right)
            {
                left = true;
            }

            yield return new WaitForSeconds(shakeHeadTuneTime);

            if (cameraControl.camstateY != CamStateY.Left && left)
            {
                menudebug.DebugMsg("ShakeHead_RightLeftRightLeft()");

                print("ShakeHead_RightLeftRightLeft()");
            }
        }

        resetShake();
    }

    void resetNod()
    {
        down = false;
        up = false;
        nodCheck = false;
    }

    void resetShake()
    { 
        left = false;
        right = false;
        shakeHead = false;
    }

}
