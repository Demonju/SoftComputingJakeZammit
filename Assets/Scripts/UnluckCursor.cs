using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnluckCursor : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject lilGuy;
    [SerializeField] GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
            pauseMenu.SetActive(true);
            player.SetActive(false);
            playerCamera.SetActive(false);
            lilGuy.SetActive(false);
            Time.timeScale = 0;
        }
    }
}
