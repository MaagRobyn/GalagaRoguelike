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
    [SerializeField] Button continueButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;

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
            SceneManager.LoadScene(Tools.SceneConstants.GAME_SCENE);
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
    }
    private void FixedUpdate()
    {
        mainCamera.transform.Rotate(new Vector3(1, 1, 0), .01f);
    }
}
