using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopAvatar : MonoBehaviour
{
    [SerializeField] private GameObject manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "avatar")
        {
            manager.GetComponent<manager>().stopWalking();
        }
    }
}
