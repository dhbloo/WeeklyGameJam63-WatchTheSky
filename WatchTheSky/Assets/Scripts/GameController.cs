using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKey(KeyCode.Z))
            StartGame();
        else if (Input.GetKey(KeyCode.X))
            EndGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Game");

        Time.timeScale = 1;

        Cursor.visible = false;
    }

    public void EndGame()
    {
        Time.timeScale = 0;

        Cursor.visible = true;
    }

}
