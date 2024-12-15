using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    public UnityEvent winner;

    public ScreenFader screenFader;

    public void Winner()
    {
        if (true == true) //this is a placeholder, fill in whatever win condition here
        {
            Debug.Log("You win!");
            winner.Invoke(); //assigned in inspector window, this is where you could make a door "open" (disappear)
        }
    }
}
