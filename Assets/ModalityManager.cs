using UnityEngine;

public class ModalityManager : MonoBehaviour
{
    public GameObject desktopCamera;
    public GameObject vrRig;
    public GameObject arSession;

    public GameObject testObject;

    void Start()
    {
        ConfigureModality();
        Debug.Log("check 1");
    }

    void ConfigureModality()
    {
        // Disable all
        desktopCamera.SetActive(false);
        vrRig.SetActive(false);
        arSession.SetActive(false);
        Debug.Log("check 2");

        // Enable based on platform
#if UNITY_STANDALONE || UNITY_EDITOR
        desktopCamera.SetActive(true);
        Debug.Log("check 3");
#elif UNITY_ANDROID || UNITY_IOS
        // Check if AR support is available
        if (IsARSupported())
        {
            arSession.SetActive(true);
        }
        else
        {
            vrRig.SetActive(true);
            testObject.SetActive(true);
        }
#elif UNITY_WSA
        vrRig.SetActive(true);
#endif
    }

    bool IsARSupported()
    {
        // Implement AR support check logic here
        // currently just returning false for testing
        return false;
    }
}
