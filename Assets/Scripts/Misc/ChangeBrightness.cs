using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChangeBrightness : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    void Start()
    {
        // Change brightness on all scene (need to be )

        postProcessVolume = GameObject.Find("PP Volume").GetComponent<PostProcessVolume>();

        if (postProcessVolume == null)
        {
            Debug.LogError("No PostProcessVolume found for brightness!");
            return;
        }

        postProcessVolume.profile.TryGetSettings(out colorGrading);

        if (colorGrading != null)
        {
            colorGrading.gamma.overrideState = true;

            colorGrading.gamma.Override(new Vector4(1f, 1f, 1f, PlayerPrefs.GetFloat("Gamma")));
        }

    }

}
