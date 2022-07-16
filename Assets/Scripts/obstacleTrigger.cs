using UnityEngine;

public class obstacleTrigger : MonoBehaviour
{
    private bool hasTriggered = false;
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" && !hasTriggered)
        {
            GameObject.Find("ObstacleGenerator").GetComponent<obstacleGenerator>().count += 1;
            hasTriggered = true;
        }
    }
}
