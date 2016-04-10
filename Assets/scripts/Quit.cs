using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {
    	
    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(8.0f);

        Application.Quit();
    }
}
