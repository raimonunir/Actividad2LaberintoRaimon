using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;

    [SerializeField] private GameObject panelStartUI;
    [SerializeField][Range(0.5f, 1.5f)] private float minimumSecondsShowingStartMenu;
    [SerializeField] private GameObject panelEndGameUI;
    [SerializeField] private GameObject panelMiniMap;

    [Header("Camera Target UI")]
    [SerializeField] private GameObject panelCameraTarget;
    [SerializeField] private Image imagePointCameraTarget;
    [SerializeField] private TextMeshProUGUI textTargetInfo;

    private Animator panelStartUIanimator;
    private bool firstClick = false;
    private bool isDesativaeTargetInfoRunning = false;
    private bool targetUIwasShowingSomething = false;

    // Start is called before the first frame update
    void Start()
    {
        // setting UI panels and animators
        panelStartUI.SetActive(true);
        panelCameraTarget.SetActive(false);
        panelEndGameUI.SetActive(false);
        panelStartUIanimator = panelStartUI.GetComponent<Animator>();
        imagePointCameraTarget.enabled = false;
        textTargetInfo.enabled = false;
        textTargetInfo.text = "";
    }

    private void Update()
    {
        // hide/show miniMap
        if (Input.GetKeyDown(KeyCode.H))
        {
            panelMiniMap.SetActive(!panelMiniMap.activeSelf);
        }

        // hide start panel UI on first click
        if (!firstClick && Input.GetKeyDown(KeyCode.Mouse0)) {
            firstClick = true;
            StartCoroutine(FadeOutStartPanelUI());
        }
    }

    private void OnEnable()
    {
        gameManagerSO.OnInteractuableObjectDetected += GameManagerSO_OnInteractuableObjectDetected;
    }

    private void GameManagerSO_OnInteractuableObjectDetected(GameManagerSO.InteractuableObjectType obj)
    {
        // activate point
        imagePointCameraTarget.enabled = true;

        // get info text
        if (obj == GameManagerSO.InteractuableObjectType.doorSwitch) {
            textTargetInfo.enabled = true;
            textTargetInfo.text = "Door Switch (Click to activate)";
            targetUIwasShowingSomething = true;
        } else if(obj == GameManagerSO.InteractuableObjectType.nothing)
        {
            imagePointCameraTarget.enabled = false;

            if (!isDesativaeTargetInfoRunning && targetUIwasShowingSomething) 
            {
                StartCoroutine(DesactivateTargetInfo());
            }

            targetUIwasShowingSomething = false;
        }



        
    }

    private IEnumerator DesactivateTargetInfo()
    {
        isDesativaeTargetInfoRunning = true;

        yield return new WaitForSeconds(1f);
        imagePointCameraTarget.enabled = false;

        textTargetInfo.enabled = false;

        isDesativaeTargetInfoRunning = false;
    }

    private void OnDisable()
    {
        gameManagerSO.OnInteractuableObjectDetected -= GameManagerSO_OnInteractuableObjectDetected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            // hide minimap, show UI end game
            panelMiniMap.SetActive(false);
            panelEndGameUI.SetActive(true);
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

        // show camera target cross
        panelCameraTarget.SetActive(true);
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