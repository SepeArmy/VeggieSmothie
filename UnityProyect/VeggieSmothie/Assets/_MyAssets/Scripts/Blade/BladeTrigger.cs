using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ps;
    [SerializeField] private GameObject playerDeadPS;
    [SerializeField] private UIManager uiManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerMovement>() != null) {
            Instantiate(playerDeadPS, other.transform.position, Quaternion.identity);
            Invoke("GameOver", 1f);
        }

        other.gameObject.SetActive(false);
        
        StartCoroutine(ParticlesSpawn(other));
    }

    IEnumerator ParticlesSpawn(Collider other) {
        for (int i = 0; i < 5; i++) {
            GameObject clon = Instantiate(ps, other.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-5, 5), 0f), Quaternion.identity);
            Destroy(clon, 2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void GameOver() {
        uiManager.GameOver();
        print("GAME OVER");
    }

}
