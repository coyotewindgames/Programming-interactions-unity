using UnityEngine;

public class ExplosiveShot : Shot
{
    public GameObject explosionEffectPrefab;
    public float explosionRadius = 5f;
    public float explosionForce = 30f;
    public float upwardModifier = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        var explosionEffect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        var rigidbody = Physics.OverlapSphere(collision.contacts[0].point, explosionRadius);
        foreach (var rb in rigidbody)
        {
            if (rb.CompareTag("Target"))
            {
                var ragdoll = rb.GetComponent<Ragdoll>();
                if(ragdoll != null)
                {
                    ragdoll.EnableRagdoll();
                    foreach (var ragdollRigidbody in ragdoll._ragdollBodies)
                    {
                        ragdollRigidbody.AddExplosionForce(explosionForce, collision.contacts[0].point, explosionRadius, upwardModifier, ForceMode.Impulse);
                    }
                }
                rb.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collision.contacts[0].point, explosionRadius, upwardModifier, ForceMode.Impulse);

            }
        }
        Destroy(gameObject);
    }

}
