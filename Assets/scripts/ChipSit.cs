using UnityEngine;
using System.Collections;

public class ChipSit : MonoBehaviour {

    float time = 1.0f;

	void Awake () {
        StartCoroutine(StopMove());
	}

    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
