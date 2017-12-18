using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionBarUI : MonoBehaviour
{

    protected float value;
    public float speed = 1;

    public bool turningOn;
    public bool turningOff;
    private bool display;

    public float rightmost;
    public float leftmost;

    private Slider bar;
    public InputControl input;

    public Image ball;
    public Image slider;
    public Image zoneAcceptation;

    public Color ballColor;
    public Color sliderColor;
    public Color zoneAcceptationColor;

    public AudioClip clipA;
    public AudioSource aS;

    private FadeMethods fade = new FadeMethods();
    public PlayerController player;

    public MenuController menu;

    private void Start()
    {
        bar = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void SetTurn() {
        turningOn = true;
        turningOff = false;
    }

    private void Update()
    {
        if (turningOn) {
            StartCoroutine(TurningOn());
        }

        if (display) {

            float progress = Mathf.Cos(speed * Time.time);
            value = Mathf.Clamp(-progress, -1, 1);

            bar.value = value;

            if (bar.value >= rightmost && bar.value <= leftmost)
            {
                if (input.action1WasPress && !turningOff)
                {
                    // print("bien");
                    StartCoroutine(TurnOff());
                }
            }

            if (bar.value < rightmost || bar.value > leftmost)
            {
                if (input.action1WasPress && !turningOff)
                {
                    player.SetLife();
                    // print("mal");

                    aS.clip = clipA;
                    aS.Play();
                    StartCoroutine(TurnOff());
                }
            }
        }
    }

    private IEnumerator TurningOn() {

        display = true;
        player.enabled = false;

        StartCoroutine(fade.ImageFadeIn(ball, ballColor));
        StartCoroutine(fade.ImageFadeIn(slider, sliderColor));
        StartCoroutine(fade.ImageFadeIn(zoneAcceptation, zoneAcceptationColor));

        yield return new WaitUntil(() => ball.color.a >= 0.95f);


        // yield return new WaitForSeconds(clipA.length);

        // menu.LoadScene(0);

        turningOn = false;
        yield break;
    }

    private IEnumerator TurnOff() {

        turningOff = true;
        player.enabled = true;

        StartCoroutine(fade.ImageFadeOut(ball));
        StartCoroutine(fade.ImageFadeOut(slider));
        StartCoroutine(fade.ImageFadeOut(zoneAcceptation));

        yield return new WaitUntil(() => ball.color.a <= 0.05f);

        display = false;
        turningOff = false;

        yield break;
    }
}
