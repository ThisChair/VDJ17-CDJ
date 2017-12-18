using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

    public AudioSource aS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            aS.Play();
            Invoke("LoadMainMenu", aS.clip.length + 2);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
