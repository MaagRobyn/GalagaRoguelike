using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] GameObject newGameMenu;
    [SerializeField] Button continueButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button endlessButton;
    [SerializeField] Button storyButton;

    [SerializeField] GameObject optionMenu;
    [SerializeField] Button closeOptionsMenuButton;
    [SerializeField] Slider volumeSlider;

    [SerializeField] Camera mainCamera;

    private void Start()
    {
        if(SoundManager.Instance == null)
        {
            SoundManager soundManager = new();
            soundManager.SetVolume(100);
        }
        volumeSlider.value = SoundManager.Instance.GetVolume();
        
        newGameButton.onClick.AddListener(() =>
        {
            newGameMenu.SetActive(true);
        });
        continueButton.onClick.AddListener(() =>
        {

        });
        optionsButton.onClick.AddListener(() =>
        {
            optionMenu.SetActive(true);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        closeOptionsMenuButton.onClick.AddListener(() =>
        {
            optionMenu.SetActive(false);
        });
        volumeSlider.onValueChanged.AddListener(x =>
        {
            SoundManager.Instance.SetVolume((int)x);
        });
        endlessButton.onClick.AddListener(() =>
        {
            GameManager.gameMode = GameManager.GameMode.Endless;
            SceneManager.LoadScene(Constants.GAME_SCENE);

        });
        storyButton.onClick.AddListener(() =>
        {
            GameManager.gameMode = GameManager.GameMode.Story;
            SceneManager.LoadScene(Constants.GAME_SCENE);

        });
    }
    private void FixedUpdate()
    {
        mainCamera.transform.Rotate(new Vector3(1, 1, 0), .01f);
    }
}
