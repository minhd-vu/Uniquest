using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayLoop("Title Screen");
    }

    public void PlayMystery()
    {
        SceneManager.LoadScene("Mystery");
    }

    public void PlayClarity()
    {
        SceneManager.LoadScene("Clarity");
    }

    public void PlayCreative()
    {
        SceneManager.LoadScene("Creative");
    }
}
