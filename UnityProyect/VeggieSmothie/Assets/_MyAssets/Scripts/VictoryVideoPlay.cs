using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VictoryVideoPlay : MonoBehaviour
{
    [SerializeField] private VideoPlayer endVideo;
    void Start()
    {
        endVideo = GetComponent<VideoPlayer>();
        endVideo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
