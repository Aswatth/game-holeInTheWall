using UnityEngine;

public class obstacleGenerator : MonoBehaviour
{
    public GameObject obstacles;
    private GameObject obstacle;

    public GameObject brokenObstacle;

    [HideInInspector]
    public string correctShape;
    [HideInInspector]
    public int count;

    private void Start()
    {
        count = 0;
    }

    public GameObject createNewObstacle(Material material,int range) {
        obstacle = Instantiate(obstacles.transform.GetChild(Random.Range(0, range)).gameObject, transform.position, transform.rotation);
        obstacle.transform.Rotate(0, 180, 0);
        obstacle.GetComponent<MeshRenderer>().material = material;
        obstacle.tag = "Obstacle";

        Rigidbody rb = obstacle.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        correctShape = this.GetComponent<obstacleData>().CreateColliders(obstacle);
        obstacle.transform.position = transform.position;
        obstacleDeathHandler death = obstacle.AddComponent<obstacleDeathHandler>();
        death.fracturedObj = brokenObstacle;

        return obstacle;
    }
    public bool checkShape() {
        print("COUNT = " + count.ToString());
        if (count.ToString() == correctShape)
        {
            count = 0;
            return true;
        }
        else {
            count = 0;
            return false;
        }
    }
}
