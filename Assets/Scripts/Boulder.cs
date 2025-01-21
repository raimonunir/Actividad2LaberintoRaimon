using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManagerSO;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if boulder is moving and collide with player
        if (rb.velocity.magnitude > 0.3f && collision.gameObject.CompareTag("Player"))
        {
            gameManagerSO.Death();
        }
    }

    public void Activate()
    {
        rb.useGravity = true;
    }
}
