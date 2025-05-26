using UnityEngine;
using UnityEngine.SceneManagement;

public class RadioStaysOnAfterTransition : MonoBehaviour
{
    /// <summary>
    /// This a script that gets attach to all radio and make the audio when transition from Autumn to Winter stays after switching scenes. It also stores the function to play the radio music, used for the rocking chair.
    /// </summary>

    public AudioSource radioAudioSource;

    private static RadioStaysOnAfterTransition instance;

    private bool didItChangeToSpatial = false;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;  // Set the static reference to this instance
        }
        else
        {
            Destroy(gameObject);  // Destroy if there is already an instance
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        didItChangeToSpatial = false;

    }

    private void Update()
    {

    }

    public static void PlayRadio()
    {
        if (RadioStaysOnAfterTransition.instance.radioAudioSource != null && !RadioStaysOnAfterTransition.instance.radioAudioSource.isPlaying)
        {
            RadioStaysOnAfterTransition.instance.radioAudioSource.Play();
        }
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Clean up to avoid memory leak
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 1)
        {
            if (radioAudioSource != null && radioAudioSource.isPlaying)
            {
                radioAudioSource.Stop();
            }
        }

        if (scene.buildIndex > 0)
        {
            RadioStaysOnAfterTransition.instance.radioAudioSource.spatialBlend = 1f;
        }

    }
}