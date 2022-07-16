using UnityEngine;

public class playerDeathHandler : MonoBehaviour
{
    public GameObject fracturedObj;
    private float breakForce = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle" || collision.collider.name == "Obstacles_fractured(Clone)")
        {
            print("Player didn't pass through");
            killAll();
        }
    }
    public void killAll() {
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in playerObjs)
        {
            p.GetComponent<playerDeathHandler>().Die();
        }
        GameObject mainPlayer = GameObject.FindGameObjectWithTag("Main Player");
        if (mainPlayer != null)
        {
            mainPlayer.GetComponent<playerDeathHandler>().Die();
        }
    }
    public void Die()
    {
        GameObject frac = Instantiate(fracturedObj, transform.position, transform.rotation);
        foreach (MeshRenderer mr in frac.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = transform.gameObject.GetComponent<MeshRenderer>().material.color;
        }
        Time.timeScale = 0.5f;
        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(breakForce, transform.position,explosionRadius: 5f);
        }
        gameObject.SetActive(false);
        gameManager.isGameOver = true;
        print("Game over");
    }

}
