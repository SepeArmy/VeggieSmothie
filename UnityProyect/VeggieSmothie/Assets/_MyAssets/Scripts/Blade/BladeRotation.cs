using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotation : MonoBehaviour
{
    [SerializeField] private Transform spinCenter;

    [SerializeField] private float spinInitialSpeed;
    [SerializeField] private float spinFinalSpeed;
    void Start()
    {
        spinCenter = transform.GetChild(0).GetChild(0);

        spinInitialSpeed = 10f;
        spinFinalSpeed = 1000f;
    }


    void Update()
    {
        if (GameManager.THIS.estoyLoco) {
            if (spinInitialSpeed < spinFinalSpeed) SpinAcceleration();
            SpinRotation();
        }
    }

    private void SpinAcceleration()
    {
        if(spinInitialSpeed < spinFinalSpeed)
        {
            spinInitialSpeed += Time.deltaTime * spinInitialSpeed;
        }
        else  spinInitialSpeed = spinFinalSpeed;
    }

    private void SpinRotation()
    {
        spinCenter.transform.Rotate(0f, spinInitialSpeed * Time.deltaTime, 0f);
    }
}
