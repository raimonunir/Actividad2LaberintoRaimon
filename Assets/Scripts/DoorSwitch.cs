using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private int idDoorSwitch;
    [SerializeField][Range(1f, 3f)] private float switchActivatedPause;

    private Animator animator;
    private bool isActive = true;


    public int IdDoorSwitch { get => idDoorSwitch;}
    public bool IsSwitchActive { get => isActive;}

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateAnimation()
    {
        if (isActive)
        {
            StartCoroutine(AnimateSwitch());
        }
    }

    private IEnumerator AnimateSwitch()
    {
        isActive = false;
        animator.SetTrigger("TriggerOpen");
        yield return new WaitForSeconds(switchActivatedPause);
        animator.SetTrigger("TriggerClose");
        yield return new WaitForSeconds(switchActivatedPause);
        isActive = true;
    }
}
