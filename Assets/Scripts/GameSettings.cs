using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    //Audio control
    private AudioSource music;
    [SerializeField] private bool musicToggle;
    [SerializeField] private bool sfxToggle;
    [SerializeField] private int gameDifficulty;

    private void Awake()
    {
        int duplicates = GameObject.FindGameObjectsWithTag("GameSettings").Length;
        if(duplicates != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        musicToggle = true;
        sfxToggle = true;
        gameDifficulty = 0;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMusic()
    {
        musicToggle = !musicToggle;
        music.enabled = musicToggle;
    }

    public bool GetMusicSetting()
    {
        return musicToggle;
    }

    public void ToggleSFX()
    {
        sfxToggle = !sfxToggle;
    }

    public bool GetSFXSetting()
    {
        return sfxToggle;
    }

    public void ToggleDifficulty()
    {
        if (gameDifficulty < 2)
        {
            gameDifficulty++;
        }
        else
        {
            gameDifficulty = 0;
        }
    }

    public int GetDifficultySetting()
    {
        return gameDifficulty;
    }
}
