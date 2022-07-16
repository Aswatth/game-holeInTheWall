using UnityEngine;

public class obstacleDeathHandler : MonoBehaviour
{
    public GameObject fracturedObj;
    private float breakForce = 500;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
            Die();

    }

    private void Die() {
        GameObject frac = Instantiate(fracturedObj, transform.position, transform.rotation);
        foreach (MeshRenderer mr in frac.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = transform.gameObject.GetComponent<MeshRenderer>().material.color;
        }
        gameObject.SetActive(false);
        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(breakForce,transform.position,explosionRadius: 5f);
        }
        gameManager.isGameOver = true;
    }
}