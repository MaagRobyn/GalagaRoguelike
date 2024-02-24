using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleTextBox;
    [SerializeField] TextMeshProUGUI subtitleTextBox;

    public string Title;
    public string Subtitle;

    private void Start()
    {
        titleTextBox.text = Title;
        subtitleTextBox.text = Subtitle;
    }
    private void Update()
    {
        if(titleTextBox.text != Title)
        {
            titleTextBox.text = Title;
        }
        if(subtitleTextBox.text != Subtitle)
        {
            subtitleTextBox.text = Subtitle;
        }
    }
}
