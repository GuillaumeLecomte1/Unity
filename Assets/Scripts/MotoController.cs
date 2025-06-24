using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MotoController : MonoBehaviour
{
    [Header("Mouvement")]
    public float acceleration = 800f;
    public float maxSpeed = 50f;
    public float turnSpeed = 3f;
    public float brakeForce = 1000f;

    [Header("Inclinaison")]
    public float tiltAmount = 15f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // abaisse le centre de masse pour plus de stabilité
    }

    void Update()
    {
        // Entrées du joueur
        moveInput = Input.GetAxis("Vertical");   // Z / S ou W / S
        turnInput = Input.GetAxis("Horizontal"); // Q / D ou A / D

        // Inclinaison visuelle de la moto
        float tilt = -turnInput * tiltAmount;
        Quaternion tiltRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, tilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, tiltRotation, Time.deltaTime * 5f);
    }

    void FixedUpdate()
    {
        // Accélération / freinage
        if (moveInput != 0 && rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * acceleration * Time.fixedDeltaTime, ForceMode.Force);
        }

        // Freinage (touche espace)
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-rb.linearVelocity.normalized * brakeForce * Time.fixedDeltaTime, ForceMode.Force);
        }

        // Rotation (tourner la moto)
        if (rb.linearVelocity.magnitude > 1f)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime * (rb.linearVelocity.magnitude / maxSpeed);
            transform.Rotate(0f, turn * 100f, 0f); // Yaw
        }
    }
}