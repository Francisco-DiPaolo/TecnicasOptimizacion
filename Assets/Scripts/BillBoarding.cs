using UnityEngine;

public class BillBoarding : MonoBehaviour
{
    Transform mainCamera;
    void Start()
    {
        mainCamera = Camera.main.gameObject.transform;
    }

    void Update()
    {
        transform.LookAt(mainCamera);
        transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
