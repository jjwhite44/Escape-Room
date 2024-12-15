using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenCondition : MonoBehaviour
{
    public Key key1;
    public Key key2;

    public TextMeshProUGUI doorLockedText;

    private PlayerController playerController;

    private Door door;

    private void Start()
    {
        door = GameObject.Find("Test Door").GetComponent<Door>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public bool ConditionMet()
    {
        if (key1.isActive() == false && key2.isActive() == false)
        {
            return true;
        }

        else
        {
            playerController.UpdateErrorText(door.errorMessage);
            StartCoroutine(DisplayLockedMessage());
            return false;
        }
    }

    IEnumerator DisplayLockedMessage()
    {
        doorLockedText.gameObject.SetActive(true); //displays locked text
        yield return new WaitForSeconds(1); //waits one second
        doorLockedText.gameObject.SetActive(false); //takes text away
    }
}
