using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{

    private Animator animator;
    public CapsuleCollider capsuleCollider;
    public List<Rigidbody> _ragdollBodies = new List<Rigidbody>();

    private List<Collider> _ragdollColliders = new List<Collider>();

    private AudioSource audioSource;

    public bool isRagdoll = false;
    public bool isCrouching = false;

    private void Awake()
    {
        
        
        TryGetComponent<Animator>(out animator);
        TryGetComponent<CapsuleCollider>(out capsuleCollider);
        TryGetComponent<AudioSource>(out audioSource);  

        if (animator == null) return;

        GetComponentsInChildren(_ragdollBodies);
        GetComponentsInChildren(_ragdollColliders);

        for (int i = 0; i < _ragdollBodies.Count; i++)
        {
            _ragdollBodies[i].isKinematic = true;
            _ragdollColliders[i].isTrigger = true;
        }
        capsuleCollider.isTrigger = false;
        if (!isCrouching)
        {
            animator.SetBool("isCrouching", true);
        }
       
    }


    public void EnableRagdoll()
    {
     isRagdoll = !isRagdoll;
        for (int i = 0; i < _ragdollBodies.Count; i++)
        {
            _ragdollBodies[i].isKinematic = false;
            _ragdollBodies[i].linearVelocity = Vector3.zero;  
            _ragdollColliders[i].isTrigger = false;
        }
        capsuleCollider.isTrigger = true;
        animator.enabled = false;
        audioSource.Play();
    }
}
