using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class TimedChildDestruct : MonoBehaviour, IDataPersistance
{

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public float destroyTime=120f;
    private bool waitingToDestroy=false;
    public bool collected = false;
    private List<GameObject> children;
    private Animator animator;
    private SpriteRenderer spriteRenderer; 
    private Collider2D coll;

    // Start is called before the first frame update
    void Awake()
    {
       this.children= getChildren();
       this.animator = gameObject.GetComponent<Animator>();
       this.spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
       this.coll = gameObject.GetComponent<Collider2D>();

    }

    public void LoadData(GameData data)
    {
        
        data.essenceCollected.TryGetValue(id, out collected);
        //Debug.Log("Loading essence with ID: " + id + " | collected = " + collected);
        if (collected)
        {
            //spriteRenderer.gameObject.SetActive(false);
            Destroy(transform.parent.gameObject);
        }
    }

    public void SaveData(ref GameData data)
    {
        //Debug.Log("Saving essence with ID: " + id + " | collected = " + collected);
        if (data.essenceCollected.ContainsKey(id))
        {
            data.essenceCollected.Remove(id);
        }
        data.essenceCollected.Add(id, collected);
    }

    void Update()
    {
        if(waitingToDestroy) {
            if(!isInCameraView(Camera.main, gameObject)) {
                Destroy(transform.parent.gameObject);
            }
        }
    }
    public void massDestruction() {
        StartCoroutine("siblingErase");
    }
    public IEnumerator siblingErase() {
        if(transform.parent.childCount > 1) {
            if(animator != null) animator.enabled=false;
            if(spriteRenderer!= null) spriteRenderer.enabled=false;
            if(coll!= null) coll.enabled=false;
            yield return new WaitForSeconds(destroyTime);
            waitingToDestroy=true;

        }
        else {
            Destroy(transform.parent.gameObject);
            yield return null;
        }
    }
    public List<GameObject> getChildren() {
        List<GameObject> tempChildren = new List<GameObject>();
        foreach(Transform child in gameObject.transform) {
            tempChildren.Add(child.gameObject);
        }
        return tempChildren;
    }
    bool isInCameraView(Camera camera, GameObject obj) {
        Vector3 viewPort= camera.WorldToViewportPoint(obj.transform.position);
        if(viewPort.x>= 0 && viewPort.x<=1.2 && viewPort.y>= 0 && viewPort.y<= 1.2) {
            return true;
        }
        else return false;
    }
}
