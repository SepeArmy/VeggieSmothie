using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSeed : MonoBehaviour
{
    [SerializeField] private SeedSelector seedSelector;
    [SerializeField] private GameObject[] seedBullet;

    [SerializeField] private float shootingSpeed;

    [SerializeField] private Transform shootingOrigin;

    private void Awake() {

    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.THIS.gameStarted) {
            if (Input.GetMouseButtonDown(0) && seedSelector.CanShootSeed(seedSelector.GetSeedSelected()) && Time.timeScale != 0.0f) {
                var v3 = Input.mousePosition;
                v3.z = -Camera.main.transform.position.z;
                v3 = Camera.main.ScreenToWorldPoint(v3);

                Vector3 shootDirection = v3 - shootingOrigin.position;
                ShootSeed(shootDirection.normalized);
            }
            else if (Input.GetMouseButtonDown(0) && Time.timeScale != 0.0f)
            {
                Debug.Log("Seed not avaliable");
                SoundManager.THIS.PlaySound(9);
            }
        }
        
    }

    private void ShootSeed(Vector3 shootDirection) {
        GameObject projectile = seedBullet[(int)seedSelector.GetSeedSelected()];
        GameObject clon = Instantiate(projectile, shootingOrigin.position, Quaternion.identity);
        SeedProjectile sp = clon.GetComponent<SeedProjectile>();
        sp.SetDirection(shootDirection);
        sp.SetInitialSpeed(shootingSpeed);

        int seedFired = (int)seedSelector.GetSeedSelected();
        seedSelector.StartColdown(seedFired);
        Debug.Log(seedFired);

        SoundManager.THIS.PlaySound(Random.Range(2, 4));

        Destroy(clon, 5f);

    }
}
