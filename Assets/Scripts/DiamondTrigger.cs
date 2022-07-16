using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondTrigger : MonoBehaviour
{
    public GameObject collectEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Instantiate(collectEffect, transform);
            print("Diamond collected");
            gameManager.diamondScoreCount += 1;
            GameObject.Find("GameManager").GetComponent<gameManager>().setDiamondCount();
            gameObject.GetComponent<MeshFilter>().mesh = null;
        }
    }
}
