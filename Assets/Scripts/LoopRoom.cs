using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LoopRoom : MonoBehaviour
{
    public Transform leftEntrance; // lijevi ulaz
    public Transform rightEntrance; // desni ulaz
    public CinemachineVirtualCamera virtualCamera; 
    public Transform roomCenter; // Ccentar sobe
    private Transform originalFollowTarget; // tamo gdje kamera inače prati 
 public int maxLoops = 10; // Maximum number of times the room can loop
    [SerializeField]
    private int currentLoopCount = 0; // Tracks how many times the player has looped
    private bool isLooping = false;
    private float startZoom;
    public float insideZoom=70f;
    public float zoomSpeed=1f;
    bool conqueredTheRoom=false;
    public Transform spiritLocation;
    public GameObject spiritEssence;
    public List<GameObject> backgroundElementsPrefabs; // Prefabs for the background elements
    private List<GameObject> activeBackgroundElements = new List<GameObject>(); // Track active elements
     public AudioSource bgm;
    public AudioSource loopRoomSoruce;
    private float originalVolume;
    public AudioClip loopRoomTheme;

    void Awake() {
        startZoom = virtualCamera.m_Lens.OrthographicSize;
         GameObject bgmObject = GameObject.Find("BGM");
        if (bgmObject != null)
        {
            bgm = bgmObject.GetComponent<AudioSource>();
            originalVolume = bgm.volume;
        }
    }
    private void Update()
    {
        // Reset loop count if the player stops trying to exit through the right
        if (!isLooping)
        {
            //StartCoroutine(FadeAudio(0f, originalVolume, 1f));
            currentLoopCount = 0;
            loopRoomSoruce.Stop();
        }
        if (currentLoopCount >= 1) { 
            //StartCoroutine(FadeAudio(originalVolume, 0f, 0f));
            if (loopRoomSoruce != null && loopRoomTheme != null)
            {
                loopRoomSoruce.PlayOneShot(loopRoomTheme);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
             if (originalFollowTarget == null)
                originalFollowTarget = virtualCamera.Follow;

            virtualCamera.Follow = roomCenter;
            StartCoroutine(AdjustZoom(insideZoom));
            if(currentLoopCount>= maxLoops) {
                spiritEssence.transform.position= spiritLocation.transform.position;
                conqueredTheRoom=true;
                StartCoroutine(FadeOutBackgroundElements());
            }

        }

    }
     private IEnumerator FadeAudio(float startVolume, float targetVolume, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            bgm.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        bgm.volume = targetVolume;
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;

            if (playerTransform.position.x > roomCenter.position.x && playerTransform.position.y < roomCenter.position.y && !conqueredTheRoom) // Exiting to the right
            {
                if (currentLoopCount <= maxLoops)
                {
                      GameObject newElement = Instantiate(backgroundElementsPrefabs[currentLoopCount], roomCenter.position, Quaternion.identity);
            SpriteRenderer renderer = newElement.GetComponent<SpriteRenderer>();
            activeBackgroundElements.Add(newElement);

                    // Teleport player back to the left entrance
                    currentLoopCount++;
                    isLooping = true;
                    playerTransform.position = leftEntrance.position;
                    
                    Debug.Log($"Looped back! Current loop count: {currentLoopCount}");
                }
                else
                {
                    // Allow the player to exit normally after max loops
                    Debug.Log("Player can now exit the room.");
                    isLooping=false;
                    virtualCamera.Follow = originalFollowTarget;
                    virtualCamera.m_Lens.OrthographicSize = startZoom;
                     StartCoroutine("FadeOutBackgroundElements");
                }
            }
            else if (isLooping) // Returning to the left
            {
                // Reset loop count if the player goes back to the left entrance
                currentLoopCount = 0;
                isLooping = false;
                Debug.Log("Loop count reset because player returned to the left.");
                             StartCoroutine(AdjustZoom(startZoom));
                  virtualCamera.Follow = originalFollowTarget;
                  StartCoroutine("FadeOutBackgroundElements");
            }
            else {
                  StartCoroutine(AdjustZoom(startZoom));
                  virtualCamera.Follow = originalFollowTarget;
                  StartCoroutine("FadeOutBackgroundElements");
            }
            }
        }
         private IEnumerator AdjustZoom(float target)
    {
        while (!Mathf.Approximately(virtualCamera.m_Lens.OrthographicSize, target))
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize, target, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }
     private IEnumerator FadeOutBackgroundElements()
    {
        foreach (GameObject element in activeBackgroundElements)
        {
            if (element != null)
            {
                SpriteRenderer renderer = element.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    float alpha = renderer.color.a;
                    while (alpha > 0)
                    {
                        alpha -= Time.deltaTime;
                        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, Mathf.Max(alpha, 0));
                        yield return null;
                    }
                }
                Destroy(element); // Remove element after fading out
            }
        }

        activeBackgroundElements.Clear(); // Clear the list
    }
    }

 