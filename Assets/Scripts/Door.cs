using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private int idDoor;
    [SerializeField] private float speed;

    private bool open = false;
    private Collider collider;
    private float sizeY;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            
        }
    }

    private void OnEnable()
    {
        gameManagerSO.OnSwitchActivated += OpenDoorEventDetected;
    }

    private void OnDisable()
    {
        gameManagerSO.OnSwitchActivated -= OpenDoorEventDetected;
    }

    private void OpenDoorEventDetected(int idSwitch)
    {
        if(idDoor == idSwitch)
        {
            StartCoroutine(OpenCloseDoorMechanic());
        }
    }

    private IEnumerator OpenCloseDoorMechanic()
    {
        float maxOpenYPosition = transform.position.y + 0.1f - collider.bounds.size.y;
        float closePosition = transform.position.y;
        float timeOpen = 3f;
        
        // open de door
        while (true)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);
            yield return null;
            if (transform.position.y <= maxOpenYPosition)
                break;
        }

        // waits for x seconds
        yield return new WaitForSeconds(timeOpen);

        // close the door
        while (true)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.up);
            yield return null;
            if (transform.position.y >= closePosition)
                break;
        }
    } 
}
