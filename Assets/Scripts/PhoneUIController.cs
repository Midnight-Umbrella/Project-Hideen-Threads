using UnityEngine;

public class PhoneUIController : MonoBehaviour
{
    public GameObject phoneUI; // Drag PhoneUI here in Inspector
    private bool PhoneIsFound = false;
    private bool isPhoneOpen = false;

    void Update()
    {
        // Press 'P' to toggle phone
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PhoneIsFound)
                TogglePhone();
        }
    }

    void TogglePhone()
    {
        isPhoneOpen = !isPhoneOpen;
        phoneUI.SetActive(isPhoneOpen);
    }

    // Trigger this when clue 2 is picked up
    void FindPhone()
    {
        PhoneIsFound = true;
    }
}