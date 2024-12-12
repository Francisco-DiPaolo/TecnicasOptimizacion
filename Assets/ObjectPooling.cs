using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    // Referencia al prefab que se va a instanciar
    public GameObject prefab;

    // Referencia al empty object que servirá como ubicación de spawn
    public Transform spawnLocation;

    // Lista de objetos activos en la escena
    private List<GameObject> activeObjects = new List<GameObject>();

    // Lista de objetos desactivados en la escena
    private List<GameObject> inactiveObjects = new List<GameObject>();

    // Variable para controlar si se están generando prefabs
    private bool isGeneratingPrefabs = false;

    // Velocidad de spawn (intervalo entre cada spawn)
    public float spawnInterval = 1f;

    // Cantidad de prefabs que se spawnearán al mismo tiempo
    public int spawnQuantity = 1;

    // Máximo número de prefabs que pueden estar presentes en la escena
    public int maxPrefabsInScene = 10;

    private void Start()
    {
        // No es necesario llamar a Clear() aquí
    }

    // Método para instanciar un nuevo objeto
    public GameObject InstantiateObject()
    {
        // Comprueba si hay objetos desactivados que se puedan reutilizar
        if (inactiveObjects.Count > 0)
        {
            GameObject obj = inactiveObjects[0];
            inactiveObjects.RemoveAt(0);
            obj.SetActive(true);
            obj.transform.position = spawnLocation.position;
            activeObjects.Add(obj);
            return obj;
        }
        else
        {
            // Si no hay objetos desactivados, crea un nuevo objeto
            if (activeObjects.Count < maxPrefabsInScene)
            {
                GameObject obj = Instantiate(prefab, spawnLocation.position, Quaternion.identity);
                activeObjects.Add(obj);
                return obj;
            }
            else
            {
                // Si ya hay demasiados prefabs en la escena, no crea uno nuevo
                return null;
            }
        }
    }

    // Método para recibir notificaciones de los prefabs cuando estén desactivados
    public void NotifyObjectDeactivated(GameObject obj)
    {
        activeObjects.Remove(obj);
        inactiveObjects.Add(obj);
    }

    // Método para spawnear un objeto
    private void SpawnObject()
    {
        for (int i = 0; i < spawnQuantity; i++)
        {
            InstantiateObject();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isGeneratingPrefabs = !isGeneratingPrefabs;
            if (!isGeneratingPrefabs)
            {
                CancelInvoke("SpawnObject");
            }
            else
            {
                InvokeRepeating("SpawnObject", 0f, spawnInterval);
            }
        }
    }
}