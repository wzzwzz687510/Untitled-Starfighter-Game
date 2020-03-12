using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string GameSceneBuildIndex;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
      mainCamera.transform.Rotate(0, Time.deltaTime, 0);
    }

    public void Play()
    {
        SceneManager.LoadScene(GameSceneBuildIndex);
    }

    public void Quit()
    {
      UnityEditor.EditorApplication.isPlaying = false;
		  Application.Quit();
	}
}
