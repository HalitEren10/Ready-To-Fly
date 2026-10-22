using UnityEngine;

public class shooter : MonoBehaviour
{
    [SerializeField]private GameObject AmmoObj;
    [SerializeField]private float Cooldown;
    [SerializeField]private Transform PlayerTransform;

    float timey;
    private void Start()
    {
        timey = Cooldown;
        if (Time.time >= Cooldown)
        {
            Cooldown += Time.time;
        }
    }
    // Update is called once per frame
    void Update()
    {
        shoot();
        transform.LookAt(PlayerTransform.position);
    }
    private void shoot()
    {
        if (Time.time >= Cooldown)
        {
            
            
            Cooldown = timey + Time.time;
            GameObject obj = Instantiate(AmmoObj, transform.position,transform.rotation);
            
            Vector3 pos = PlayerTransform.position;
            obj.GetComponent<Ammo>().pTransform = pos;
            
            
            Destroy(obj,20);
            
        }
    }
}
