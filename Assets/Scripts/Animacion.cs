using UnityEngine;

public class Animacion : MonoBehaviour
{
    public float distance = 2f;
    public float speed = 2f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPosition + new Vector3(0, 0, offset);
    }
}
