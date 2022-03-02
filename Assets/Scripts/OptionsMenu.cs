using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameSettings;
    [SerializeField] private Button musicToggle;
    [SerializeField] private Button sfxToggle;

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
        }
    }
}
