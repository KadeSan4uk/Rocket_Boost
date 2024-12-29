using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            ApplyRotation(rotationSpeed);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
    }

    private void ProcessThrust()
    {
        if (thrust.IsInProgress())
        {
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime);
        }
    }
}
