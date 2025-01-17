using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameManagerSO")]
public class GameManagerSO : ScriptableObject
{

    public enum InteractuableObjectType {doorSwitch, nothing};

    [SerializeField][Range(3f, 6f)] private float secondsToQuitAppAffterWin;


    // events
    public event Action<int> OnSwitchActivated;
    public event Action<InteractuableObjectType> OnInteractuableObjectDetected;

    // Switch has been activated
    public void SwitchActivated(int idSwitch)
    {
        OnSwitchActivated?.Invoke(idSwitch);
    }

    public void InfoUI(InteractuableObjectType interactuableObject)
    {
        OnInteractuableObjectDetected?.Invoke(interactuableObject);
    }
}
