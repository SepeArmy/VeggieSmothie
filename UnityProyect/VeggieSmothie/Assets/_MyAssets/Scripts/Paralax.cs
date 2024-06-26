using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Paralax : MonoBehaviour
{
    [SerializeField] Transform player;

    RectTransform t;
    void Start()
    {
        t = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        t.localPosition = new Vector3(player.position.x * -43, player.position.y * -50 - 560, 0f);
        //t.rect.Set(player.position.x * -43, player.position.y * -43, 4000, 6000);
    }
}
