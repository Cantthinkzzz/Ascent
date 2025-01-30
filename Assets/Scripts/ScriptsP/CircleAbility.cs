using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAbility : MonoBehaviour
{
    public PlayerController playerController;
    public AudioSource audioSource;
    public float effectRadius = 5f; 
    public float expansionSpeed = 3f; 
    public float hazardInvisibleTime = 5f; 
    public bool isClickAllowed = true;
    public float debounceTime = 3f;

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component attached to this object
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("No LineRenderer found on the GameObject.");
        }
    }

    public void Circle()
    {
        if (isClickAllowed)
        {
            Debug.Log("KRUG");
            StartCoroutine(ActivateCircleEffect());
            StartCoroutine(DebounceClick());
        }
    }

    private IEnumerator DebounceClick()
    {
        isClickAllowed = false;
        yield return new WaitForSeconds(debounceTime);
        isClickAllowed = true;
    }

    private IEnumerator ActivateCircleEffect()
    {
        Debug.Log("Circle effect");
        // Start expanding the circle

        float currentRadius = 0f;
         if (audioSource != null && playerController.circleSound != null)
        {
            audioSource.PlayOneShot(playerController.circleSound);
        }
        while (currentRadius < effectRadius)
        {
            currentRadius += expansionSpeed * Time.deltaTime;
            DrawCircle(currentRadius);
            HideHazardsInRange(currentRadius);

            yield return null;
        }
        Debug.Log("Vrati hazarde");
        yield return new WaitForSeconds(hazardInvisibleTime);

        // Restore hazards after 5 seconds
        ClearCircle();
        RestoreHazards();

    }

    private void ClearCircle()
    {
        // Set the position count to 0 to remove the line renderer's points
        lineRenderer.positionCount = 0;
    }
    private List<GameObject> disabledHazards = new List<GameObject>();
    private void HideHazardsInRange(float radius)
    {
        // Find all objects with the "Hazard" tag
        Collider2D[] hazards = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hazard in hazards)
        {
            if (hazard.CompareTag("Hazard"))
            {
                // Temporarily disable the hazard (make it invisible)
                hazard.gameObject.SetActive(false);

                // Store the hazard in the disabled list if not already added
                if (!disabledHazards.Contains(hazard.gameObject))
                {
                    disabledHazards.Add(hazard.gameObject);
                }
            }
        }
    }

    private void RestoreHazards()
    {
  
        foreach (var hazard in disabledHazards)
        {
            if (hazard != null)
            {
                hazard.SetActive(true); 
                Debug.Log("Restoring hazard: " + hazard.name); 
            }
        }

        
        disabledHazards.Clear();
    }

    private void DrawCircle(float radius)
    {
        int segments = 100; // Number of segments for the circle
        lineRenderer.positionCount = segments; // Set the number of points on the line

        // Calculate the current width based on the radius
        float width = Mathf.Lerp(100f, 0f, radius / effectRadius); // Lerp between 10 (initial) and 0 (final) based on the radius

        lineRenderer.startWidth = width;  // Set the start width of the circle
        lineRenderer.endWidth = width;    // Set the end width of the circle

        for (int i = 0; i < segments; i++)
        {
            float angle = (i * 360f / segments) * Mathf.Deg2Rad;
            Vector3 point = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0) + transform.position;
            lineRenderer.SetPosition(i, point);
        }
    }
}
