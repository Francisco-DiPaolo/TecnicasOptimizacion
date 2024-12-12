using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    // Referencia al ObjectPooler
    public ObjectPooler objectPooler;

    // Tiempo de vida del objeto antes de desactivarse
    public float lifeTime = 5f;

    private void Start()
    {
        // Inicia el contador de tiempo
        Invoke("DeactivateObject", lifeTime);
    }

    // Método para desactivar el objeto
    private void DeactivateObject()
    {
        gameObject.SetActive(false);
        objectPooler.NotifyObjectDeactivated(gameObject);
    }
}