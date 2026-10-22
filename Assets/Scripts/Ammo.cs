using UnityEngine;


public class Ammo : MonoBehaviour
{
    public Vector3 pTransform;
    public Vector3 dist;
    [SerializeField]private float Speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (pTransform == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.LookAt(pTransform);
            transform.position = Vector3.MoveTowards(transform.position, pTransform, Speed * Time.deltaTime);
        }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,pTransform +  transform.forward * 10,Speed * Time.deltaTime);
        if (pTransform +  transform.forward * 10 == transform.position)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Shooter")
        {
            Destroy(gameObject);
        }
    }
}
