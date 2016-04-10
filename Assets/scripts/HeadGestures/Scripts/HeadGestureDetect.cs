/* HeadGestureDetect.cs
 * (c) 2014 Quantum Leap Computing (QLC)
 * Author: Dave Arendash
 */
 
 using UnityEngine;
using System.Collections;

public class HeadGestureDetect : MonoBehaviour
{	
	public float nodSensitivity = 10f;
	public float shakeSensitivity = 10f;
	public float leanSensitivity = 10f;
	public float nodNeutralZone = 5f;
	public float shakeNeutralZone = 5f;
	public float leanNeutralZone = 5f;
	public float timeOut = 3f;
	public float doubleNodTimeOut = 1.5f;
	
	public Transform trackedHead = null;
	public GameObject trackerTarget = null;

	float countdown = 0f;
	
	float baselineX = 0f;
	float baselineY = 0f;
	float baselineZ = 0f;

	float pitch = 0f;
	float yaw = 0f;
	float roll = 0f;
	float doSomethingTime = 0f;
	
	bool tiltUp = false;
	bool tiltDown = false;
	bool nodUp = false;
	bool nodDown = false;

	bool turnLeft = false;
	bool turnRight = false;
	bool shakeLeft = false;
	bool shakeRight = false;
	
	bool leanLeft = false;
	bool leanRight = false;
	bool nodLeft = false;
	bool nodRight = false;
	
	int nods = 0;
	int shakes = 0;
	int sideNods = 0;
	
	float pitchAng;
	float yawAng;
	float rollAng;

    AndyCamImpl andyCam;
	
	void Start ()
	{
		if (trackedHead == null)
			trackedHead = transform;
		if (trackerTarget == null)
			trackerTarget = gameObject;
		baselineX = Angle(trackedHead.localEulerAngles.x);
		baselineY = Angle(trackedHead.localEulerAngles.y);
		baselineZ = Angle(trackedHead.localEulerAngles.z);
		doSomethingTime = Time.time;

        GameObject temp = null;
                
        if(GameObject.Find("GodOB") != null)
        { 
            temp = GameObject.Find("GodOB");
        }
        else
        {
            temp = GameObject.Find("GOD");
        }

        andyCam = temp.GetComponent<AndyCamImpl>();
    }
	
	void Update ()
	{
		countdown = Time.time - doSomethingTime;
		pitch = Angle(trackedHead.localEulerAngles.x);
		yaw = Angle(trackedHead.localEulerAngles.y);
		roll = Angle(trackedHead.localEulerAngles.z);

		pitchAng = AngleBetween (pitch, baselineX);
		yawAng = AngleBetween (yaw, baselineY);
		rollAng = AngleBetween (roll, baselineZ);
		
		// Head held high for too long
		if (tiltUp && countdown > timeOut)
		{
			Debug.Log ("head held high too long");
			tiltUp = false;
			baselineX = pitch;
			return;
		}
		
		// Head held low for too long
		if (tiltDown && countdown > timeOut)
		{
			Debug.Log ("head held low too long");
			tiltDown = false;
			baselineX = pitch;
			return;
		}

		// Head held left for too long
		if (turnLeft && countdown > timeOut)
		{
			Debug.Log ("head held left too long");
			turnLeft = false;
			baselineY = yaw;
			return;
		}

		// Head held right for too long
		if (turnRight && countdown > timeOut)
		{
			Debug.Log ("head held right too long");
			turnRight = false;
			baselineY = yaw;
			return;
		}
		
		if (nods > 0 && countdown > timeOut)
			nods = 0;
		if (shakes > 0 && countdown > timeOut)
			shakes = 0;
		if (sideNods > 0 && countdown > timeOut)
			sideNods = 0;
		
		// Handle head tilt/nod
		// Not yet 'tiltUp' but reached a good angle, and not because the baseline crept up
		if (!tiltUp && pitchAng < -nodSensitivity)
		{
			tiltUp = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadTiltUp",SendMessageOptions.DontRequireReceiver);
		}
		
		// Already tilted up, and we've pitched back down, and not because the baseline crept up
		if (tiltUp && pitchAng > nodNeutralZone && countdown < timeOut)
		{
			tiltUp = false;
			nodUp = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadNodUp",SendMessageOptions.DontRequireReceiver);
		}
		// need to clear nodUp after a while if no other nod
		// need to not set nodUp again until small time passed (hysteresis)

		if (!tiltDown && pitchAng > nodSensitivity)
		{
			tiltDown = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadTiltDown",SendMessageOptions.DontRequireReceiver);
		}

		if (tiltDown && pitchAng < nodNeutralZone && countdown < timeOut)
		{
			tiltDown = false;
			doSomethingTime = Time.time;
			nodDown = true;
			trackerTarget.SendMessage ("HeadNodDown",SendMessageOptions.DontRequireReceiver);
		}

		if (nodDown && nodUp)
		{
			nods++;
			trackerTarget.SendMessage ("HeadFullNod", nods, SendMessageOptions.DontRequireReceiver);
			doSomethingTime = Time.time;
			nodUp = false;
			nodDown = false;
		}

		
		// Handle head turn/shake
		// Not yet 'turnRight' but reached a good angle
		if (!turnRight && yawAng > shakeSensitivity)
		{
			turnRight = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadTurnRight",SendMessageOptions.DontRequireReceiver);
		}
		
		// Already turned right, and we've yawed back to forward
		if (turnRight && yawAng < shakeNeutralZone && countdown < timeOut)
		{
			turnRight= false;
			shakeRight = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadShakeRight",SendMessageOptions.DontRequireReceiver);
		}
		
		if (!turnLeft && yawAng < -shakeSensitivity)
		{
			turnLeft = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadTurnLeft",SendMessageOptions.DontRequireReceiver);
		}
		
		if (turnLeft && yawAng > -shakeNeutralZone)
		{
			turnLeft = false;
			doSomethingTime = Time.time;
			shakeLeft = true;
			trackerTarget.SendMessage ("HeadShakeLeft",SendMessageOptions.DontRequireReceiver);
		}
		
		if (shakeLeft && shakeRight)
		{
			shakes++;
			trackerTarget.SendMessage ("HeadFullShake",shakes,SendMessageOptions.DontRequireReceiver);
			doSomethingTime = Time.time;
			shakeLeft = false;
			shakeRight = false;
		}


		// Handle head lean
		if (!leanRight && rollAng < -leanSensitivity)
		{
			leanRight = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadLeanRight",SendMessageOptions.DontRequireReceiver);
		}
		
		if (leanRight && rollAng > leanNeutralZone && countdown < timeOut)
		{
			leanRight= false;
			nodRight = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadNodRight",SendMessageOptions.DontRequireReceiver);
		}
		
		if (!leanLeft && rollAng > leanSensitivity)
		{
			leanLeft = true;
			doSomethingTime = Time.time;
			trackerTarget.SendMessage ("HeadLeanLeft",SendMessageOptions.DontRequireReceiver);
		}
		
		if (leanLeft && rollAng < leanNeutralZone)
		{
			leanLeft = false;
			doSomethingTime = Time.time;
			nodLeft = true;
			trackerTarget.SendMessage ("HeadNodLeft",SendMessageOptions.DontRequireReceiver);
		}
		
		if (nodLeft && nodRight)
		{
			sideNods++;
			trackerTarget.SendMessage ("HeadFullSideNod",sideNods,SendMessageOptions.DontRequireReceiver);
			doSomethingTime = Time.time;
			nodLeft = false;
			nodRight = false;
		}
	}
	
	float Angle (float a)
	{
		if (a > 180f)
			a = a - 360f;
		return a;
	}

	float AngleBetween (float a, float b)
	{
		float d = a - b;
		if (d > 180f || d < -180f)
			d = 360f - d;
		return d;
	}
	
	void HeadTiltUp ()
	{
        andyCam.HeadTiltUp();
	}

	void HeadTiltDown ()
	{
        andyCam.HeadTiltDown();
	}

	void HeadNodUp ()
	{
        andyCam.HeadNodUp();
	}

	void HeadNodDown ()
	{
        andyCam.HeadNodDown();
    }

	void HeadFullNod ()
	{
        andyCam.HeadFullNod();
    }
    
	void HeadTurnLeft ()
	{
        andyCam.HeadTurnLeft();
    }

	void HeadTurnRight ()
	{
        andyCam.HeadTurnRight();
    }
	
	void HeadShakeLeft ()
	{
        andyCam.HeadShakeLeft();
    }

	void HeadShakeRight ()
	{
        andyCam.HeadShakeRight();
    }
	
	void HeadFullShake ()
	{
        andyCam.HeadFullShake();
	}
    
	void HeadLeanLeft ()
	{
        andyCam.HeadLeanLeft();
    }

	void HeadLeanRight ()
	{
        andyCam.HeadLeanRight();
    }
	
	void HeadNodLeft ()
	{
        andyCam.HeadNodLeft();
    }

	void HeadNodRight ()
	{
        andyCam.HeadNodRight();
    }
	
	void HeadFullSideNod ()
	{
        andyCam.HeadFullSideNod();
    }
	
}
