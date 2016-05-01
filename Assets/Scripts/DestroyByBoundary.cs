using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")//might not need this
        {
            return;
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}