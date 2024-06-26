using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAscension : MonoBehaviour
{
    [SerializeField] private float initialSpeed;
    [SerializeField] private float finalSpeed;

    private bool startAscending;


    void Start()
    {
        startAscending = false;
        initialSpeed = 0.001f;
        finalSpeed = 0.5f;
        Invoke("StartAscension", 3f);
    }

    
    void Update()
    {
        if(GameManager.THIS.gameStarted) {
            if (initialSpeed < finalSpeed && startAscending) Acceleration();
            if (startAscending) Ascension();
        }

    }

    private void Acceleration()
    {
        if (initialSpeed < finalSpeed)
        {
            initialSpeed += Time.deltaTime * 0.01f;
        }
        else initialSpeed = finalSpeed;
    }

    private void Ascension()
    {
        transform.position += Vector3.up * initialSpeed * Time.deltaTime;
    }

    private void StartAscension()
    {
        startAscending = true;
    }
}
