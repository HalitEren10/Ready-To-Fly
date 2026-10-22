using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction Thrust;
    [SerializeField] private InputAction Rotation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float ThrustPower;
    [SerializeField] private float RotationPower;
    private AudioSource audioSource;
    [SerializeField]private AudioClip EngineSfx;

    
    [SerializeField]private ParticleSystem mainThrusterParticle;
    [SerializeField]private ParticleSystem LeftSideThrusterParticle;
    [SerializeField]private ParticleSystem RightSideThrusterParticle;
    

    
    private void OnEnable()
    {
        Thrust.Enable();
        Rotation.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        QuitGame();
    }
    private void FixedUpdate()
    {
        MoveFunction();
        RotationFunction();
    }
    private void MoveFunction()
    {
        if (Thrust.IsPressed())
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * ThrustPower * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {

            audioSource.PlayOneShot(EngineSfx);
        }
        if (!mainThrusterParticle.isPlaying)
        {
            mainThrusterParticle.Play();
        }
    }
    private void StopThrusting()
    {
        mainThrusterParticle.Stop();
        audioSource.Stop();
    }

   

    private void RotationFunction()
    {
        float rotationInput = Rotation.ReadValue<float>();
        
        //more joystick control player can use tiny movements to rotate less
        //transform.Rotate(Vector3.forward * rotationInput * Time.fixedDeltaTime * -1);

        //for it to be same for both keyboard and gamepad
        if (rotationInput < 0)
        {
            RotateLeft();
        }
        else if (rotationInput > 0)
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    private void RotateLeft()
    {
        if (!RightSideThrusterParticle.isPlaying)
        {
            LeftSideThrusterParticle.Stop();
            RightSideThrusterParticle.Play();
        }

        ApplyRotation(RotationPower);
    }

    private void RotateRight()
    {
        if (!LeftSideThrusterParticle.isPlaying)
        {
            RightSideThrusterParticle.Stop();
            LeftSideThrusterParticle.Play();
        }

        ApplyRotation(-RotationPower);
    }

    private void StopRotating()
    {
        LeftSideThrusterParticle.Stop();
        RightSideThrusterParticle.Stop();
    }





    private void ApplyRotation(float RotationValue)
    {
        //rb.freezeRotation = true;
        rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        transform.Rotate(Vector3.forward * Time.deltaTime * RotationValue);
        //rb.freezeRotation = false;
        rb.constraints &=  ~RigidbodyConstraints.FreezeRotationZ;
    }

    private void QuitGame()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("no way u r leaving me");
            Application.Quit();
        }
    }
}
