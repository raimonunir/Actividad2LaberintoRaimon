using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallTrap : MonoBehaviour
{
    
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;
    [SerializeField][Range(1000f, 100000f)] private float wallTrapForce;

    private WallTrapWall wallTrapWallLeft;
    private WallTrapWall wallTrapWallRight;
    private Rigidbody wallLeftRigidBody;
    private Rigidbody wallRightRigidBody;
    private bool wallTrapActivated = false;

    private void Start()
    {
        wallTrapWallLeft = wallLeft.GetComponent<WallTrapWall>();
        wallTrapWallRight = wallRight.GetComponent<WallTrapWall>();
        wallLeftRigidBody = wallLeft.GetComponent<Rigidbody>();
        wallRightRigidBody = wallRight.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            wallTrapActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            wallTrapActivated = false;
        }
    }

    private void Update() 
    {
        /*
        if (wallTrapActivated)
        {
            wallRight.transform.Translate(Time.deltaTime * wallTrapForce * wallRight.transform.forward);
            wallLeft.transform.Translate(Time.deltaTime * -wallTrapForce * wallLeft.transform.forward);
            if(wallTrapWallLeft.isTouchingPlayer && wallTrapWallRight.isTouchingPlayer)
            {
                gameManagerSO.Death();
            }
        }
        */
    }

    private void FixedUpdate()
    {
        if (wallTrapActivated)
        {
            wallRightRigidBody.AddForce(wallRight.transform.forward * wallTrapForce, ForceMode.Force);
            wallLeftRigidBody.AddForce(wallLeft.transform.forward * -wallTrapForce, ForceMode.Force);
            if (wallTrapWallLeft.isTouchingPlayer && wallTrapWallRight.isTouchingPlayer)
            {
                gameManagerSO.Death();
            }
        }
    }

}
