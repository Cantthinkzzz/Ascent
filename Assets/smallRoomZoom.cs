using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;

public class smallRoomZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float endZoom;
    private float startZoom;
    private float beforeZoom;
    public float zoomDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        startZoom = virtualCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        beforeZoom = virtualCamera.m_Lens.OrthographicSize;
        if(other.CompareTag("Player")) {
            StartCoroutine(ZoomIn());
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        beforeZoom = virtualCamera.m_Lens.OrthographicSize;
        if(other.CompareTag("Player")) {
            StartCoroutine(ZoomOut());
        }
    }
    private IEnumerator ZoomIn() {
        float currentTime = 0f;
        while(currentTime <= zoomDuration) {
           float factor= currentTime/zoomDuration;
           virtualCamera.m_Lens.OrthographicSize=Mathf.Lerp(beforeZoom, endZoom, factor);
           currentTime+= Time.deltaTime;
           yield return null;
        }

 

    }
    private IEnumerator ZoomOut() {
        float currentTime = 0f;
        while(currentTime <= zoomDuration) {
           float factor= currentTime/zoomDuration;
           virtualCamera.m_Lens.OrthographicSize=Mathf.Lerp(beforeZoom, startZoom, factor);
           currentTime+= Time.deltaTime;
           yield return null;
        }
    }
}
