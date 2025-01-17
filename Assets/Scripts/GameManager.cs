using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            //StartCoroutine(ExitAppAfterSeconds(secondsToQuitAppAffterWin));
            //Time.timeScale = 0f;
        }
    }


    /*
    private IEnumerator ExitAppAfterSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds); //XXX Realtime is not affected by Time.timeScale
        Application.Quit();
    }
    */

    /****
     * This function is called from an Event Trigger from PanelStart
     ****/

}
