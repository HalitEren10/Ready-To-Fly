using UnityEngine;

public class oscillator : MonoBehaviour
{
    Vector3 startPos;
    Vector3 EndPos;
    [SerializeField]float Speed;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        EndPos = startPos + movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time*Speed ,1);
        transform.position = Vector3.Lerp(startPos,EndPos,movementFactor);
    }
}
