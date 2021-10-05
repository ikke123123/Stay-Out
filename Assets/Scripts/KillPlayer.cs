using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private DisplayText displayText = null;
    [SerializeField] private string activatedGodMode = "Press 'F' to disable God Mode, and 'F' to interact with objects.";
    [SerializeField] private string deactivedGodMode = "Press 'F' to enable God Mode, and 'F' to interact with objects.";
    [SerializeField] private KeyCode godModeKey = KeyCode.G;

    private bool godModeActivated = false;

    private void Update()
    {
        if (Input.GetKeyDown(godModeKey))
        {
            godModeActivated = CodeLibrary.FlipBool(godModeActivated);
            displayText.TextDisplay(godModeActivated ? activatedGodMode : deactivedGodMode);
        }
    }

    public void Kill()
    {
        Debug.Log("Very Dead");
        if (godModeActivated == false) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
