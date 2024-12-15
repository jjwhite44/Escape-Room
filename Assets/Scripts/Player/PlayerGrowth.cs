using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrowth : MonoBehaviour
{
    public ScreenFader screenFader;

    InputActions inputActions;//this refers to the player input, which we can use to access interact key (E or left click)

    public float inputDelay;

    private void Awake()
    {
        inputActions = new InputActions(); //initializes player input
        inputDelay = screenFader.fadeDuration * 3;
    }

    public float timer = 0; //leave this value alone, this is just to start counting at 0

    private void Update()
    {
        timer += Time.deltaTime; //track passing game time
        if (timer >= inputDelay)
        {
            if (inputActions.Player.Grow.IsPressed()) //if player presses interact key
            {
                timer = 0;
                StartCoroutine(Growth());
            }
        }
    }

    private IEnumerator Growth()
    {
        screenFader.FadeIn();
        yield return new WaitForSeconds(screenFader.fadeDuration);

        // Perform the growth logic here (if any)
        yield return new WaitForSeconds(screenFader.fadeDuration); // Simulate some growth logic duration

        screenFader.FadeOut();
        yield return new WaitForSeconds(screenFader.fadeDuration);
    }

    public void OnEnable()
    {
        inputActions.Player.Enable(); //here Player is referring to the name of the action map in Unity
    }

    public void OnDisable()
    {
        inputActions.Player.Disable(); //here Player is referring to the name of the action map in Unity
    }
}
