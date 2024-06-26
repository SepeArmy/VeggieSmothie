using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SeedSelector : MonoBehaviour
{
    public enum ESeeds {
        SeedEnredadera = 0,
        SeedRama = 1,
        SeedSeta = 2
    }


    private ESeeds seedSelected;

    [SerializeField] private bool[] seedsAvarible;

    [SerializeField] private GameObject[] seedSlots;
    [SerializeField] private float[] seedsColdowns;
    [SerializeField] private float[] seedsColdownsMaxTime;



    [SerializeField] private Sprite marco;
    [SerializeField] private Sprite marcoResaltado;
    [SerializeField] private Sprite[] iconosSemillas;

    [SerializeField] private UIManager uiManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetSeedSelected(ESeeds.SeedEnredadera);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SetSeedSelected(ESeeds.SeedRama);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SetSeedSelected(ESeeds.SeedSeta);
        }

        if(Input.mouseScrollDelta.y < 0 && (int)GetSeedSelected() < 2)
            SetSeedSelected((ESeeds)((int)GetSeedSelected() + 1));
        if (Input.mouseScrollDelta.y > 0 && (int)GetSeedSelected() > 0)
            SetSeedSelected((ESeeds)((int)GetSeedSelected() - 1));

        SeedsColdown();
    }

    public ESeeds GetSeedSelected() { return seedSelected; }
    public void SetSeedSelected(ESeeds seed) {
        if (seedsAvarible[(int)seed]) {
            seedSelected = seed;
            foreach (var item in seedSlots) {
                item.GetComponent<Image>().sprite = marco;
                item.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            }

            seedSlots[(int)seed].GetComponent<Image>().sprite = marcoResaltado;
            seedSlots[(int)seed].GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    public void UnlockSeed(int seed) {
        seedsAvarible[seed] = true;
        seedSelected = (ESeeds)seed;
        seedSlots[seed].transform.GetChild(0).GetComponent<Image>().sprite = iconosSemillas[seed];
        seedSlots[seed].transform.GetChild(0).GetComponent<Image>().color = Color.white;
        foreach (var item in seedSlots) {
            item.GetComponent<Image>().sprite = marco;
            item.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        }

        

        seedSlots[(int)seed].GetComponent<Image>().sprite = marcoResaltado;
        seedSlots[(int)seed].GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
        StartCoroutine(scaleAnim(seedSlots[(int)seed].GetComponent<Transform>()));
    }

    IEnumerator scaleAnim(Transform t) {
        while(t.localScale.x < 2.5f) {
            yield return new WaitForSeconds(0);
            t.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        }
        while (t.localScale.x > 1.5f) {
            yield return new WaitForSeconds(0);
            t.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
        }


    }

    public bool CanShootSeed(ESeeds seed) {
        
        
        return seedsAvarible[(int)seed] && seedsColdowns[(int)seed] <= 0f;
    }

    public void StartColdown(int seed)
    {
        seedsColdowns[seed] = seedsColdownsMaxTime[seed];
    }

    private void SeedsColdown()
    {
        for (int i = 0; i < 3; i++)
        {
            if (seedsColdowns[i] > 0f)
            {
                seedsColdowns[i]  -= Time.deltaTime;
                if (i == 0) uiManager.RefreshEnredaderaRefill(seedsColdowns[i] / seedsColdownsMaxTime[i]);
                if (i == 1) uiManager.RefreshRamaRefill(seedsColdowns[i] / seedsColdownsMaxTime[i]);
                if (i == 2) uiManager.RefreshSetaRefill(seedsColdowns[i] / seedsColdownsMaxTime[i]);
            } else if (seedsColdowns[i] <= 0f)
            {
                //SoundManager.THIS.PlaySound(7);
                seedsColdowns[i] = 0f;
            }
            
        }
    }
}
