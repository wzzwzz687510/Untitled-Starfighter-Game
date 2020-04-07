using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Management")]
    public GameObject loadingScreen;
    public TextMeshProUGUI progressText;
    public GameObject titleElement;
    public GameObject functionalityElement;
    [Header("Misc.")]
    public GameObject mainCamera;
    public AudioMixer audioMixer;
    [Header("Quality Settings (Buttons)")]
    public Button increaseQualityBtn;
    public Button decreaseQualityBtn;
    [Header("Quality Settings (Text)")]
    public GameObject lowQualityText;
    public GameObject mediumQualityText;
    public GameObject highQualityText;

    void Start()
    {
        //progressText = GetComponent<TextMeshProUGUI>();
        print(Screen.currentResolution);
    }

    void Update()
    {
        mainCamera.transform.Rotate(0, Time.deltaTime, 0);
    }

    void LateUpdate()
    {
        if (QualitySettings.GetQualityLevel() == 0) {
            lowQualityText.SetActive(true);
            mediumQualityText.SetActive(false);
            highQualityText.SetActive(false);
            decreaseQualityBtn.interactable = false;
        }
        else if (QualitySettings.GetQualityLevel() == 1) {
            mediumQualityText.SetActive(true);
            lowQualityText.SetActive(false);
            highQualityText.SetActive(false);
            decreaseQualityBtn.interactable = true;
            increaseQualityBtn.interactable = true;
        }
        else if (QualitySettings.GetQualityLevel() == 2) {
            highQualityText.SetActive(true);
            lowQualityText.SetActive(false);
            mediumQualityText.SetActive(false);
            increaseQualityBtn.interactable = false;
        }
    }

    void Awake()
    {
        increaseQualityBtn.onClick.AddListener(IncreaseQuality);
        decreaseQualityBtn.onClick.AddListener(DecreaseQuality);
    }

    void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel(false);
    }

    void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel(false);
    }

    public void NewGame(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //titleElement.SetActive(false);
        //functionalityElement.SetActive(false);
        loadingScreen.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", GainToDB(volume));
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", GainToDB(volume));
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", GainToDB(volume));
    }

    private float GainToDB(float volume)
    {
        return 6 * Mathf.Log(volume,2);
    }
}
