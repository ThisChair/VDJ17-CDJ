using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public InputControl input;
    public GameObject pausedUI;

    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (input.action5WasPress) {

            pausedUI.SetActive(true);
            player.SetPause();
        }    
    }

    public void ResumeGame() {
        pausedUI.SetActive(false);
        player.SetPause();
    }
}
