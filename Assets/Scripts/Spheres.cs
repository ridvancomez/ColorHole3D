using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spheres : MonoBehaviour
{
    //k�pr�deki toplar
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
}
