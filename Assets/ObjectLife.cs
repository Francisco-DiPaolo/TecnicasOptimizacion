using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLife : MonoBehaviour
{
    public float lifeTime;

    void Update()
    {
        // Restar tiempo de vida
        lifeTime -= Time.deltaTime;

        // Si el tiempo de vida se agota, apagar el objeto
        if (lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
