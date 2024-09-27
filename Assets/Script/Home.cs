using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public static Home home;
    public TMP_InputField inputField;

    public string player_name;

    private void Awake()
    {
        if (home == null)
        {
            home = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerName()
    {
        player_name = inputField.text;
        SceneManager.LoadSceneAsync("Level");
    }
}
