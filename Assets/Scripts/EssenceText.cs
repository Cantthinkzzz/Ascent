using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceText : MonoBehaviour, IDataPersistance
{
    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool collected = false;

    public string question ="do you accept?";
    public string option1 ="yes";
    public string option2="no";
    public bool leftFight= true;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void LoadData(GameData data)
    {

        data.spiritEssenceCollected.TryGetValue(id, out collected);
        //Debug.Log("Loading spirit essence with ID: " + id + " | collected = " + collected);
        if (collected)
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }

    public void SaveData(ref GameData data)
    {
       //Debug.Log("Saving spirit essence with ID: " + id + " | collected = " + collected);
        if (data.spiritEssenceCollected.ContainsKey(id))
        {
            data.spiritEssenceCollected.Remove(id);
        }
        data.spiritEssenceCollected.Add(id, collected);
    }

}
