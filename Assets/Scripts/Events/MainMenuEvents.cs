using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MainMenuEvents : MonoBehaviour
{
    /// <summary>
    /// Holds all the events called on animations or button clicks in the main menu.
    /// </summary>

    public Animator mainMenuAnimator;

    public bool clickedSomething = false;
    public AudioSource mainMenuAudioSource;
    public AudioClip clickedSomethingClip;

    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;
    public Slider gammaSlider;

    // Since it is already on the main menu, Imma use it to make sure the curso appears.
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        postProcessVolume.profile.TryGetSettings(out colorGrading);

        if (gammaSlider != null)
        {
            gammaSlider.onValueChanged.AddListener(ChangeBrightness);
            ChangeBrightness(gammaSlider.value); // Initialize
        }
    }

    public void TransitionChooseGender()
    {
        if (!clickedSomething)
        {
            mainMenuAudioSource.PlayOneShot(clickedSomethingClip, 0.2f);
            mainMenuAnimator.SetBool("ChooseGender", true);
            clickedSomething = true;
        }
    }

    public void BackFromChooseGender()
    {
        if (!clickedSomething)
        {
            mainMenuAudioSource.PlayOneShot(clickedSomethingClip, 0.2f);
            mainMenuAnimator.SetBool("ChooseGender", false);
            clickedSomething = true;
        }
    }

    public void MaleChooseGender()
    {
        if (!clickedSomething)
        {
            mainMenuAudioSource.PlayOneShot(clickedSomethingClip, 0.2f);
            PlayerPrefs.SetInt("Gender", 0);
            clickedSomething = true;
        }
    }

    public void FemaleChooseGender()
    {
        if (!clickedSomething)
        {
            mainMenuAudioSource.PlayOneShot(clickedSomethingClip, 0.2f);
            PlayerPrefs.SetInt("Gender", 1);
            clickedSomething = true;
        }
    }

    public void DoneBrightness()
    {
        mainMenuAudioSource.PlayOneShot(clickedSomethingClip, 0.2f);
        mainMenuAnimator.SetTrigger("PlayGame");
        clickedSomething = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        if (!clickedSomething)
        {
            mainMenuAudioSource.PlayOneShot(clickedSomethingClip, 0.2f);
            mainMenuAnimator.SetTrigger("QuitGame");
            clickedSomething = true;
        }
    }

    public void ActuallyQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    public void YouCanClickAgainNow()
    {
        clickedSomething = false;
    }

    public void ChangeBrightness(float gammaValue)
    {
        if (colorGrading != null)
        {
            colorGrading.gamma.overrideState = true;

            // Convert gamma value to log space for Unity's ColorGrading
            //float logGamma = Mathf.Log(gammaValue) * 20f;

            colorGrading.gamma.Override(new Vector4(1f, 1f, 1f, gammaValue));

            //colorGrading.gamma.value = new Vector4(gammaValue, gammaValue, gammaValue, 0f);

            //Debug.Log($"Gamma adjusted: Input = {gammaValue}, Log = {logGamma}");
        }

        PlayerPrefs.SetFloat("Gamma", gammaValue);
    }
}
