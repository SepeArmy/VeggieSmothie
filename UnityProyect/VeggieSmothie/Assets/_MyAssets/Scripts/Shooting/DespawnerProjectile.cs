using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerProjectile : MonoBehaviour
{
    private Rigidbody rb;

    private float initialSpeed;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void SetInitialSpeed(float speed) { rb.AddForce(speed * direction, ForceMode.Impulse); }
    public void SetDirection(Vector3 dir) { direction = dir; }
}
