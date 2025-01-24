using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField][Range(10f, 100000f)] private float initialImpulseForce;
    [SerializeField][Range(1f, 5f)][Tooltip("When the boulder speed is under this value it can not kill")] 
    private float minimumKillingSpeed;
    
    private bool isKilling = false;
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
        if (isKilling && collision.gameObject.CompareTag("Player"))
        {
            gameManagerSO.Death();
        }
    }

    private IEnumerator StopKilling()
    {
        // wait 1 second
        yield return new WaitForSeconds(1f);

        // wait until speed decreases
        yield return new WaitUntil(() => rb.velocity.magnitude < minimumKillingSpeed);

        Debug.Log("No more kills");

        // stop script
        isKilling = false ;
        rb.angularDrag = 0.5f;

        yield return new WaitUntil(() => rb.velocity.magnitude < 0.02f);
        Debug.Log("No more movement");

        rb.isKinematic = true ;
    }

    public void Activate()
    {
        isKilling = true;
        StartCoroutine(StopKilling());
        rb.useGravity = true;
        rb.AddForce(Vector3.down * initialImpulseForce, ForceMode.Impulse);
    }


}
