using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public GameObject cammy;

    public CamStateX camstateX;
    public CamStateY camstateY;

    float shakeHeadNo = 0.05f;
    float nodHeadYes = 0.05f;

    void Start()
    {
        camstateX = CamStateX.Straight;
        camstateY = CamStateY.Straight;
    }

    void Update () {

        if (cammy.transform.localRotation.x <= -nodHeadYes)
            camstateX = CamStateX.Up;
        else if (cammy.transform.localRotation.x >= nodHeadYes)
            camstateX = CamStateX.Down;
        else
            camstateX = CamStateX.Straight;


        if (cammy.transform.localRotation.y <= -shakeHeadNo)
            camstateY = CamStateY.Left;
        else if (cammy.transform.localRotation.y >= shakeHeadNo)
            camstateY = CamStateY.Right;
        else
            camstateY = CamStateY.Straight;
    }
}

