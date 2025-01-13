using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject panelStartUI;
    [SerializeField] [Range(0.5f, 1.5f)] private float minimumSecondsShowingStartMenu;
    [SerializeField] private GameObject panelEndGameUI;
    [SerializeField] private GameObject panelMiniMap;
    [SerializeField] [Range(3f, 6f)] private float secondsToQuitAppAffterWin;

    private Animator panelStartUIanimator;


    // Start is called before the first frame update
    void Start()
    {
        // setting UI panels and animators
        panelEndGameUI.SetActive(false);
        panelStartUIanimator = panelStartUI.GetComponent<Animator>();
    }

    private void Update()
    {
        // hide/show miniMap
        if (Input.GetKeyDown(KeyCode.H))
        {
            panelMiniMap.SetActive(!panelMiniMap.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            // hide minimap, show UI end game
            panelMiniMap.SetActive(false);
            panelEndGameUI.SetActive(true);
            StartCoroutine(ExitAppAfterSeconds(secondsToQuitAppAffterWin));
            Time.timeScale = 0f;
        }
    }

    private IEnumerator FadeOutStartPanelUI()
    {
        float secondsOffsetAfterAnimationEnds = 1f;

        // fade out start menu
        yield return new WaitForSeconds(minimumSecondsShowingStartMenu);
        panelStartUIanimator.SetTrigger("FadeOut");

        // cursor locked
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        // wait for fadeout
        yield return new WaitForSeconds(panelStartUIanimator.GetCurrentAnimatorStateInfo(0).length + secondsOffsetAfterAnimationEnds);

        // disable start menu
        panelStartUI.SetActive(false);

        // show minimap
        panelMiniMap.SetActive(true);
    }

    private IEnumerator ExitAppAfterSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds); //XXX Realtime is not affected by Time.timeScale
        Application.Quit();
    }

    /****
     * This function is called from an Event Trigger from PanelStart
     ****/
    public void StartGame()
    {
        // fade out and disable UI start menu after x seconds
        StartCoroutine(FadeOutStartPanelUI());
    }
}
