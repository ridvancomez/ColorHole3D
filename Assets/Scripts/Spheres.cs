using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spheres : MonoBehaviour
{
    //köprüdeki toplar
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
}
