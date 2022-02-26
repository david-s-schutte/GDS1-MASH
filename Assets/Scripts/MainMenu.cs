using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Button references
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;

    private AudioSource sfx;

    // Start is called before the first frame update
    void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }

    public void AnimateButton(Button buttonToAnimate)
    {
        //Enable the animator controller
        buttonToAnimate.gameObject.GetComponent<Animator>().enabled = true;
    }

    public void FreezeButtonAnimation(Button buttonToFreeze)
    {
        //Disable the animator controller and reset the rotation of the button
        buttonToFreeze.gameObject.GetComponent<Animator>().enabled = false;
        buttonToFreeze.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
