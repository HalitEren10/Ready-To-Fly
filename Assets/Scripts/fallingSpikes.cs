using UnityEngine;

public class fallingSpikes : MonoBehaviour
{
    [SerializeField] private float FallTimer;
    [SerializeField] private GameObject rb;
    private float fallTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Time.time >= FallTimer)
        {
            FallTimer += Time.time;
        }
        fallTime = FallTimer;

    }

    // Update is called once per frame
    void Update()
    {
        Falling();
    }

    private void Falling()
    {
        if (Time.time >= FallTimer)
        {
            Instantiate(rb,gameObject.transform.position,transform.rotation);
            FallTimer = Time.time + fallTime;
        }
    }
}
