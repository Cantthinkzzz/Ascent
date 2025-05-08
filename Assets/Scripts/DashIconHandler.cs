using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIconHandler : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject unlockedDashIcon;

    private bool isDashingIconUsed = false;

    void Start()
    {
        unlockedDashIcon.SetActive(false);
    }

    void Update()
    {
        if (playerController.unlockedDash)
        {
            unlockedDashIcon.SetActive(true);

            if (playerController.usedAirDash && !isDashingIconUsed)
            {
                unlockedDashIcon.transform.Find("NotUsedDash").gameObject.SetActive(false);
                unlockedDashIcon.transform.Find("UsedDash").gameObject.SetActive(true);
                isDashingIconUsed = true;
                Debug.Log("Dashing icon set to Used");
            }
            else if (!playerController.usedAirDash && isDashingIconUsed)
            {
                unlockedDashIcon.transform.Find("NotUsedDash").gameObject.SetActive(true);
                unlockedDashIcon.transform.Find("UsedDash").gameObject.SetActive(false);
                isDashingIconUsed = false;
                Debug.Log("Dashing icon set to NotUsed");
            }
        }
        else
        {
            unlockedDashIcon.SetActive(false);
            Debug.Log("DASH DEAKTIVAJ");
        }
    }
}

