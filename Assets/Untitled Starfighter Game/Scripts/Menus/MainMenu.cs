using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public int GameSceneBuildIndex;
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
