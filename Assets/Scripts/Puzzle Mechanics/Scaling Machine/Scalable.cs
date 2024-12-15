using UnityEngine;

public class Scalable : MonoBehaviour
{
    public float scaleFactorUp;
    public float scaleFactorDown;
    public int scaleLevelsUp;
    public int scaleLevelsDown;

    public int currentScaleLevel;

    public void ScaleUp()
    {
        if (currentScaleLevel < scaleLevelsUp)
        {
            transform.localScale *= scaleFactorUp;
            currentScaleLevel++;
        }
    }

    public void ScaleDown()
    {
        if (currentScaleLevel > -scaleLevelsDown)
        {
            transform.localScale *= scaleFactorDown;
            currentScaleLevel--;
        }
    }
}
