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

    private void Start()
    {
        newGameButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Tools.SceneConstants.GAME_SCENE);
        });
        continueButton.onClick.AddListener(() =>
        {

        });
        optionsButton.onClick.AddListener(() =>
        {

        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
