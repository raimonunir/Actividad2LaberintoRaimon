using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TNTtrap : MonoBehaviour
{

    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private ParticleSystem wickParticleSystem;
    [SerializeField] private ParticleSystem explosionParticleSystemOne;
    [SerializeField] private ParticleSystem explosionParticleSystemTwo;
    [SerializeField] private ParticleSystem explosionCylinder;
    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject wick;
    [SerializeField] private GameObject explosionStain;
    [SerializeField][Range(4f, 10f)] private float letalRange;

    private Animator animationController;


    // Start is called before the first frame update
    void Start()
    {
        animationController = GetComponent<Animator>();
    }

    public void ActivateWick()
    {
        wickParticleSystem.gameObject.SetActive(true); 
        wickParticleSystem.Play();
        animationController.SetTrigger("TriggerWick");
    }

    public void Explosion()
    {
        Debug.Log("Explosion!!!!");
        explosionStain.SetActive(true);
        Destroy(wick);
        Destroy(cylinder);
        wickParticleSystem.Stop();
        wickParticleSystem.gameObject.SetActive(false);
        explosionParticleSystemOne.Play();
        explosionParticleSystemTwo.Play();
        explosionCylinder.Play();

        gameManagerSO.Shake();

        Collider[] colliders = Physics.OverlapSphere(transform.position, letalRange);

        foreach (Collider collider in colliders) {
            if (collider.CompareTag("Player"))
            {
                gameManagerSO.Death();
            }
        }
    }

    
}
