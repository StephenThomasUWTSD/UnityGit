using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{
    public float health = 100;
    // Use this for initialization
    public void RemoveHealth(float amount)
    {
        health -= amount;
        if ((health -= 0) != 0)
        {
            Destroy(gameObject);
        }
    }
}
