using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Prefab del objeto que se va a generar
    public GameObject prefab;

    // Tamaño inicial del pool
    public int initialSize = 10;

    // Tamaño máximo del pool
    public int maxSize = 20;

    // Tiempo de vida del objeto en segundos
    public float lifeTime = 5f;

    // Frecuencia de spawneo (en segundos)
    public float spawnFrequency = 1f;

    // Lista de objetos en el pool
    private List<GameObject> pool;

    // Estado del spawneo (true = activo, false = inactivo)
    private bool isSpawning = false;

    // Punto de spawn
    public Transform spawnPoint;

    void Start()
    {
        // Inicializar el pool
        pool = new List<GameObject>();

        // Generar objetos iniciales en el pool
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            GetObject();
            yield return new WaitForSeconds(spawnFrequency); // Esperar la frecuencia de spawneo antes de spawnear el próximo objeto
        }
    }

    // Método para obtener un objeto del pool
    public GameObject GetObject()
    {
        // Si el spawneo está activo, obtener un objeto del pool
        if (isSpawning)
        {
            // Buscar un objeto inactivo en el pool
            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    // Activar el objeto y restarle el tiempo de vida
                    obj.SetActive(true);
                    obj.transform.position = spawnPoint.position;
                    obj.transform.rotation = spawnPoint.rotation;
                    obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // Resetear el momentum
                    obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // Resetear el momentum angular
                    obj.GetComponent<ObjectLife>().lifeTime = lifeTime;
                    return obj;
                }
            }

            // Si no hay objetos inactivos, crear uno nuevo
            if (pool.Count < maxSize)
            {
                GameObject obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                obj.SetActive(true);
                obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // Resetear el momentum
                obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // Resetear el momentum angular
                obj.GetComponent<ObjectLife>().lifeTime = lifeTime;
                pool.Add(obj);
                return obj;
            }

            // Si el pool está lleno, devuelve null
            return null;
        }
        else
        {
            // Si el spawneo está inactivo, devuelve null
            return null;
        }
    }

    // Método para devolver un objeto al pool
    public void ReturnObject(GameObject obj)
    {
        // Desactivar el objeto y agregarlo al pool
        obj.SetActive(false);
        pool.Add(obj);
    }

    // Método para togglear el spawneo
    void Update()
    {
        // Si se presiona la letra "P", togglear el spawneo
        if (Input.GetKeyDown(KeyCode.P))
        {
            isSpawning = !isSpawning;

            if (isSpawning)
            {
                StartCoroutine(SpawnLoop());
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }
}
