using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GameManagerSO")]
public class GameManagerSO : ScriptableObject
{

    public enum InteractuableObjectType {doorSwitch, nothing};

    [SerializeField][Range(3f, 6f)] private float secondsToQuitAppAffterWin;


    // events
    public event Action<int> OnSwitchActivated;
    public event Action<InteractuableObjectType> OnInteractuableObjectDetected;
    public event Action OnVictory;
    public event Action OnDeath;

    private bool m_isAlive = true;

    public bool isAlive {  get => m_isAlive;  }

    // Switch has been activated
    public void SwitchActivated(int idSwitch)
    {
        OnSwitchActivated?.Invoke(idSwitch);
    }

    public void InfoUI(InteractuableObjectType interactuableObject)
    {
        OnInteractuableObjectDetected?.Invoke(interactuableObject);
    }

    public void Victory()
    {
        OnVictory?.Invoke();
    }

    public void Death()
    {
        OnDeath?.Invoke();
        m_isAlive = false;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetAlive()
    {
        m_isAlive = true;
    }
}
