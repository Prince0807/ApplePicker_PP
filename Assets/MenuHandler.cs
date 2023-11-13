using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public Toggle musicOnOff;
    public Slider volume;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LOAD_SCENE(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnMusicToggleUpdate()
    {
        audioMixer.SetFloat("Volume", musicOnOff.isOn? 0 : -80);
    }
    public void OnVolumeChange()
    {
        audioMixer.SetFloat("Volume", volume.value);
    }

    public void PauseGame(bool status)
    {
        Time.timeScale = status? 1 : 0;
        pausePanel.SetActive(!status);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(false);
        }
    }
}
