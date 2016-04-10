using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AndyCamImpl : MonoBehaviour {

    public enum CamState { None,Nodded,HeadShook };

    public CamState cameraState = CamState.None;

    public GameObject test;
    
    public void db(string s)
    {
        //debugLeft.text = s;
        //debugRight.text = s;
    }

    public void HeadTiltUp()
    {
        //Debug.Log("tilt up");
        //db("tilt up");
        cameraState = CamState.None;
    }

    public void HeadTiltDown()
    {
        //Debug.Log("tilt down at ");
        //db("tilt down at ");
        cameraState = CamState.None;
    }

    public void HeadNodUp()
    {
        //Debug.Log("nod up ");
        //db("nod up ");
        cameraState = CamState.None;
    }

    public void HeadNodDown()
    {
        //Debug.Log("nod down");
        //db("nod down");
        cameraState = CamState.None;
    }

    public void HeadFullNod()
    {
        //Debug.Log("full nod");
        //db("full nod");
        cameraState = CamState.Nodded;
    }

    public void HeadTurnLeft()
    {
        //Debug.Log("turn left");
        //db("turn left");
        cameraState = CamState.None;
    }

    public void HeadTurnRight()
    {
        //Debug.Log("turn right");
        //db("turn right");
        cameraState = CamState.None;
    }

    public void HeadShakeLeft()
    {
        //Debug.Log("shake left");
        //db("shake left");
        cameraState = CamState.None;
    }

    public void HeadShakeRight()
    {
        //Debug.Log("shake right");
        //db("shake right");
        cameraState = CamState.None;
    }

    public void HeadFullShake()
    {
        //Debug.Log("full shake");
        //db("full shake");
        cameraState = CamState.HeadShook;
    }

    public void HeadLeanLeft()
    {
        //ebug.Log("lean left");
        //db("lean left");
        cameraState = CamState.None;
    }

    public void HeadLeanRight()
    {
        //Debug.Log("lean right");
        //db("lean right");
        cameraState = CamState.None;
    }

    public void HeadNodLeft()
    {
        //Debug.Log("nod left");
        //db("nod left");
        cameraState = CamState.None;
    }

    public void HeadNodRight()
    {
        //Debug.Log("nod right");
        //db("nod right");
        cameraState = CamState.None;
    }

    public void HeadFullSideNod()
    {
        //Debug.Log("full side nod");
        //db("full side nod");
        cameraState = CamState.None;
    }
}
