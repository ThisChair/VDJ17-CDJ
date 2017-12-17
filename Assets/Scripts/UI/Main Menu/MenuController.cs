using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public InputControl input;
    public OptionsReferences uiItemSeletected;

    public AudioSource audioSource;
    public AudioClip accept;
    public AudioClip tap;

    public GameObject loadingCircle;
    public bool useAlphaBg;

    public virtual void Update()
    {
        bool inputSubmit = input.verticalInput;

        if (inputSubmit && !loadingCircle.activeSelf)
        {

            uiItemSeletected.image.color = !useAlphaBg? Color.black : new Color (0, 0, 0, 0);
            uiItemSeletected.text.color = Color.white;

            if (input.verticalInput && input.verticalDir == 1) { MoveFocusTo(uiItemSeletected.up); }
            if (input.verticalInput && input.verticalDir == -1) { MoveFocusTo(uiItemSeletected.down); }
            // if (horizontalInput && horizontalDir == 1) { MoveFocusTo(uiItemSeletected.right); }
            // if (horizontalInput && horizontalDir == -1) { MoveFocusTo(uiItemSeletected.left); }

            uiItemSeletected.image.color = Color.white;
            uiItemSeletected.text.color = Color.black;
        }

        if (Input.GetButtonDown("Jump") && !loadingCircle.activeSelf)
        {
            uiItemSeletected.button.onClick.Invoke();
        }
    }


    public void MoveFocusTo(OptionsReferences newFocusedButton)
    {
        if (newFocusedButton != null)
        {
            uiItemSeletected = newFocusedButton;
            audioSource.clip = accept;
            audioSource.Play();
        }

        else {
            audioSource.clip = tap;
            audioSource.Play();
        }
    }

    public void LoadScene(int sceneIndex) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadSceneAsync(string scene)
    {
        // you MUST start ALL coroutines before the loadasync call
        StartCoroutine(ChangeScene(scene));
    }

    IEnumerator ChangeScene(string scene)
    {
        loadingCircle.SetActive(true);

        yield return new WaitForSeconds(2.4f);
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void Quit() {
        Application.Quit();
    }
}
