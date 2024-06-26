using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineVirtualCamera secCamera;
        
    void Start()
    {
        mainCamera.Priority = 1;
        secCamera.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart() {
        StartCoroutine(CameraCoroutine());
    }

    IEnumerator CameraCoroutine() {
        secCamera.Priority = 2;
        yield return new WaitForSeconds(2f);
        GameManager.THIS.estoyLoco = true;
        yield return new WaitForSeconds(5f);

        secCamera.Priority = 0;
        yield return new WaitForSeconds(2f);
        GameManager.THIS.gameStarted = true;
    }
}
