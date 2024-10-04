using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuScript : ControlsScript
{
    public GameObject pauseMenu;
    public GameObject userInterface;
    public float pauseValue;
    public bool paused;

    // Start is called before the first frame update
    protected override void Start()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        userInterface.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void Awake()
    {
        base.Awake();
        menu.Enable();
    }

    protected override void OnEnable()
    {
        back.Enable();

        back.started += OnPause;
    }

    protected override void OnDisable()
    {
        back.Disable();

        back.started -= OnPause;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            userInterface.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            userInterface.SetActive(true);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Continue()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        userInterface.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
