using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
public class PlayerMovement : MonoBehaviour
{

    // globals
    [SerializeField][Range(5f, 20f)] private float movementSpeed;
    private CharacterController characterController;

    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // walk movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * x + transform.forward * z).normalized;
        characterController.Move (movementSpeed * Time.deltaTime * direction);

        //XXX rotation is in camera MouseLook component
    }
}
