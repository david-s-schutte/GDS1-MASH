using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameSettings;
    [SerializeField] private Button musicToggle;
    [SerializeField] private Button sfxToggle;
    [SerializeField] private Button difficultyButton;

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        UpdateText();
    }
    
    public void ToggleMusic()
    {
        if (gameSettings)
        {
            gameSettings.GetComponent<GameSettings>().ToggleMusic();
            UpdateText();
        }
    }

    public void ToggleSFX()
    {
        if (gameSettings)
        {
            gameSettings.GetComponent<GameSettings>().ToggleSFX();
            UpdateText();
        }

    }

    public void ToggleGameDifficulty()
    {
        if (gameSettings)
        {
            gameSettings.GetComponent<GameSettings>().ToggleDifficulty();
            UpdateText();
        }
    }

    private void UpdateText()
    {
        if (gameSettings)
        {
            //Update the Music Button
            if (gameSettings.GetComponent<GameSettings>().GetMusicSetting())
            {
                musicToggle.transform.Find("Text").gameObject.GetComponent<Text>().text = "Music: On";
            }
            else
            {
                musicToggle.transform.Find("Text").gameObject.GetComponent<Text>().text = "Music: Off";
            }
            //Update the SFX button
            if (gameSettings.GetComponent<GameSettings>().GetSFXSetting())
            {
                sfxToggle.transform.Find("Text").gameObject.GetComponent<Text>().text = "SFX: On";
            }
            else
            {
                sfxToggle.transform.Find("Text").gameObject.GetComponent<Text>().text = "SFX: Off";
            }
            //Update the difficulty button
            switch (gameSettings.GetComponent<GameSettings>().GetDifficultySetting())
            {
                case 0: 
                    difficultyButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "Difficulty: Easy"; 
                    break;
                case 1:
                    difficultyButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "Difficulty: Medium";
                    break;
                case 2:
                    difficultyButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "Difficulty: Hard";
                    break;
                default: break;
            }
        }
    }
}
