using UnityEngine;

public class noGravity : MonoBehaviour
{
    [SerializeField]private GameObject noGravObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noGravObj.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
