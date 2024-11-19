using UnityEngine;
using UnityEngine.UI;

public class QuitGameOnKeypress : MonoBehaviour {
	
	public KeyCode key = KeyCode.Escape;
	public GameObject settingsCanvas;

    void Update () {
		if(Input.GetKeyDown(key))
		{
            settingsCanvas.SetActive(true);
        }
	}
}