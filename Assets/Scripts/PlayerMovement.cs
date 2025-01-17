using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
public class PlayerMovement : MonoBehaviour
{

    // globals
    [SerializeField] private GameManagerSO gameManagerSO;

    [Header("Movement")]
    [SerializeField][Range(5f, 20f)] private float movementSpeed;
    [SerializeField][Range(1f, 10f)] private float jumpHeight;

    [Header("GroundCheckGravity")]
    [SerializeField] private Transform checkGroundEmpty;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] [Range(0.2f, 0.8f)] private float groundCheckDistance;

    [Header("Raycast")]
    [SerializeField] private LayerMask layermaskRaycast;
    [SerializeField][Range(10f, 40f)] private float maxRaycastDistance;

    private CharacterController characterController;
    private Camera cameraPlayer;
    private float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraPlayer = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // walk movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * x + transform.forward * z).normalized;
        characterController.Move (movementSpeed * Time.deltaTime * direction);

        // gravity
        isGrounded = Physics.CheckSphere(checkGroundEmpty.position, groundCheckDistance, groundLayerMask);
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; // basado en el tutorial que nos pasaron en clase
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //XXX rotation is in cameraPlayer MouseLook component

        // raycast
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out RaycastHit hitInfo, maxRaycastDistance, layermaskRaycast)) {
                
                Debug.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * maxRaycastDistance, Color.red, 10f);
                if (hitInfo.collider.TryGetComponent(out DoorSwitch doorSwitch))
                {
                    if(doorSwitch.IsSwitchActive)
                    {
                        doorSwitch.ActivateAnimation();
                        gameManagerSO.SwitchActivated(doorSwitch.IdDoorSwitch);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out RaycastHit hitInfo, maxRaycastDistance, layermaskRaycast))
        {
            if (hitInfo.collider.TryGetComponent(out DoorSwitch doorSwitch))
            {
                gameManagerSO.InfoUI(GameManagerSO.InteractuableObjectType.doorSwitch);
                Debug.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * maxRaycastDistance, Color.yellow);

            }
            else
            {
                gameManagerSO.InfoUI(GameManagerSO.InteractuableObjectType.nothing);
                Debug.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * maxRaycastDistance, Color.white);
            }
        }
        else
        {
            gameManagerSO.InfoUI(GameManagerSO.InteractuableObjectType.nothing);
            Debug.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * maxRaycastDistance, Color.white);
        }
    }
}
