using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDespawner : MonoBehaviour
{

    [SerializeField] private GameObject despawnBullet;

    [SerializeField] private float shootingSpeed;

    [SerializeField] private Transform shootingOrigin;

    [SerializeField] private float maxCooldown;
    private float cooldown;

    [SerializeField] UIManager uiManager;
   

  
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.timeScale != 0.0f)
        {
            if(cooldown <= 0) {
                var v3 = Input.mousePosition;
                v3.z = -Camera.main.transform.position.z;
                v3 = Camera.main.ScreenToWorldPoint(v3);

                Vector3 shootDirection = v3 - shootingOrigin.position;
                ShootDespawner(shootDirection.normalized);
            }
            
        }
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
            uiManager.RefreshDespawnRefill(cooldown / maxCooldown);
        }
    }

    private void ShootDespawner(Vector3 shootDirection)
    {
        GameObject clon = Instantiate(despawnBullet, shootingOrigin.position, Quaternion.identity);
        DespawnerProjectile dp = clon.GetComponent<DespawnerProjectile>();
        dp.SetDirection(shootDirection);
        dp.SetInitialSpeed(shootingSpeed);

        cooldown = maxCooldown;
    }

    public float GetCooldown() { return cooldown; }
}
