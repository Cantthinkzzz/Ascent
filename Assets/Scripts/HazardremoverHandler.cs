using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardremoverHandler : MonoBehaviour
{
   public PlayerController playerController;
    public GameObject unlockedHazardRemoverIcon;
    public CircleAbility circleAbility;

    private bool isHazardRemoverIconUsed = false;

    void Start()
    {
        unlockedHazardRemoverIcon.SetActive(false);
    }

    void Update()
    {
        if (playerController.unlockedCircle)
        {
            unlockedHazardRemoverIcon.SetActive(true);

            if (!circleAbility.isClickAllowed && !isHazardRemoverIconUsed)
            {
                unlockedHazardRemoverIcon.transform.Find("NotUsedHazard").gameObject.SetActive(false);
                unlockedHazardRemoverIcon.transform.Find("UsedHazard").gameObject.SetActive(true);
                isHazardRemoverIconUsed = true;
            }
            else if (circleAbility.isClickAllowed && isHazardRemoverIconUsed)
            {
                unlockedHazardRemoverIcon.transform.Find("NotUsedHazard").gameObject.SetActive(true);
                unlockedHazardRemoverIcon.transform.Find("UsedHazard").gameObject.SetActive(false);
                isHazardRemoverIconUsed = false;
            }
        }
        else
        {
            unlockedHazardRemoverIcon.SetActive(false);
        }
    }
}
