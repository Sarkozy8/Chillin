using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// A singleton made to store all the audio FX and Dialogues for the game.
    /// With this, at any point, any scene can play the dialogue they want.
    /// Just need to drop the singleton, and assign the 2 audio sources (can be the same one for scenes where there are no footsteps).
    /// </summary>

    // Steps to make a singleton so all the FX sounds can be played by anyscripts
    public static SoundManager Instance { get; private set; }
    private static bool hasLaunched = false;

    // Where the sound is coming from?
    public AudioSource audioSourceFX;
    public AudioSource audioSourceFootSteps;
    private float pitchMax = 0.75f;
    private float pitchMin = 1.1f;

    // Interacts
    [Header("Interacts")]
    public AudioClip badJobClip;
    public AudioClip goodJobClip;
    public AudioClip completeJobClip;
    public AudioClip rockingChairClip;
    public AudioClip throwBookClip;
    public AudioClip pickupBookClip;

    // Walking
    [Header("Walking")]
    public AudioClip cabinStep;
    public AudioClip autumnStep;
    public AudioClip winterStep;
    public AudioClip grassStep;

    // Fishing
    [Header("Fishing")]
    public AudioClip splashFishingClip;
    public AudioClip reelFishingClip;

    // Dialogue Female
    [Header("Dialogues Female")]
    public AudioClip deerFemale;
    public AudioClip BookAlchemistFemale;
    public AudioClip BookSpaceFemale;
    public AudioClip Crops1Female;
    public AudioClip Crops2Female;
    public AudioClip Crops3Female;
    public AudioClip CropsDoneFemale;
    public AudioClip ChillingFemale;
    public AudioClip Fail1Female;
    public AudioClip Fail2Female;
    public AudioClip Win1Female;
    public AudioClip Pond1Female;
    public AudioClip Pond2Female;
    public AudioClip Pond3Female;
    public AudioClip AutumnToothBrushFemale;
    public AudioClip SpringToothFemale;
    public AudioClip SummerToothFemale;
    public AudioClip WinterToothFemale;
    public AudioClip Well1Female;
    public AudioClip Well2Female;
    public AudioClip Furnace1Female;
    public AudioClip Furnace2Female;
    public AudioClip Furnace3Female;

    [Header("Dialogues Male")]
    public AudioClip deerMale;
    public AudioClip BookAlchemistMale;
    public AudioClip BookSpaceMale;
    public AudioClip Crops1Male;
    public AudioClip Crops2Male;
    public AudioClip Crops3Male;
    public AudioClip CropsDoneMale;
    public AudioClip ChillingMale;
    public AudioClip Fail1Male;
    public AudioClip Fail2Male;
    public AudioClip Win1Male;
    public AudioClip Pond1Male;
    public AudioClip Pond2Male;
    public AudioClip Pond3Male;
    public AudioClip AutumnToothBrushMale;
    public AudioClip SpringToothMale;
    public AudioClip SummerToothMale;
    public AudioClip WinterToothMale;
    public AudioClip Well1Male;
    public AudioClip Well2Male;
    public AudioClip Furnace1Male;
    public AudioClip Furnace2Male;
    public AudioClip Furnace3Male;

    private bool toggleCrops = false;

    // Ensures only one instance of SightingManager exists in the scene
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  // Set the static reference to this instance
        }
        else
        {
            Destroy(gameObject);  // Destroy if there is already an instance
        }
    }

    private void Start()
    {
        // Noticed that I needed to put the main menu in scene 0 but too late for that so when the game launches, it will jump the main menu
        if (!hasLaunched)
        {
            hasLaunched = true;
            SceneManager.LoadScene(6);
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // "Scollers" for interact dialogues
            PlayerPrefs.SetInt("FurnaceInteract", 1);
            PlayerPrefs.SetInt("WellInteract", 1);
            PlayerPrefs.SetInt("PondInteract", 1);
            PlayerPrefs.SetInt("CropsInteract", 1);
        }
    }

    private void Update()
    {
        // "Scollers" for interact dialogues

        if (PlayerPrefs.GetInt("FurnaceInteract") > 3)
            PlayerPrefs.SetInt("FurnaceInteract", 1);

        if (PlayerPrefs.GetInt("WellInteract") > 2)
            PlayerPrefs.SetInt("WellInteract", 1);

        if (PlayerPrefs.GetInt("PondInteract") > 3)
            PlayerPrefs.SetInt("PondInteract", 1);

        if (PlayerPrefs.GetInt("CropsInteract") > 3)
            PlayerPrefs.SetInt("CropsInteract", 1);

        // PLays the crops dialogue after all the crops are harvested
        if (PlayerPrefs.GetInt("cropsHarvested") > 4 && !toggleCrops)
        {
            toggleCrops = true;

            if (PlayerPrefs.GetInt("Gender") == 0)
            {
                PlayDialogue(AudioDialogue.CropsDoneMale);
            }
            else
            {
                PlayDialogue(AudioDialogue.CropsDoneFemale);
            }

        }

    }

    public static void PlayFXSound(AudioFXSounds clipName)
    {
        switch (clipName)
        {
            case AudioFXSounds.BadJob:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.badJobClip, .2f);
                break;
            case AudioFXSounds.GoodJob:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.goodJobClip, .2f);
                break;
            case AudioFXSounds.CompleteJob:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.completeJobClip, .2f);
                break;
            case AudioFXSounds.RockingChair:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.pitch = 0.7f;
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.rockingChairClip, .005f);
                break;
            case AudioFXSounds.ThrowBook:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.throwBookClip, .05f);
                break;
            case AudioFXSounds.PickupBook:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.pickupBookClip, .05f);
                break;
            case AudioFXSounds.CabinStep:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.RandomizeValues();
                SoundManager.Instance.audioSourceFootSteps.PlayOneShot(SoundManager.Instance.cabinStep, .08f);
                break;
            case AudioFXSounds.AutumnStep:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.RandomizeValues();
                SoundManager.Instance.audioSourceFootSteps.PlayOneShot(SoundManager.Instance.autumnStep, .005f);
                break;
            case AudioFXSounds.WinterStep:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.RandomizeValues();
                SoundManager.Instance.audioSourceFootSteps.PlayOneShot(SoundManager.Instance.winterStep, .05f);
                break;
            case AudioFXSounds.GrassStep:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.RandomizeValues();
                SoundManager.Instance.audioSourceFootSteps.PlayOneShot(SoundManager.Instance.grassStep, .05f);
                break;
            case AudioFXSounds.splashFishing:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.splashFishingClip, .05f);
                break;
            case AudioFXSounds.reelFishing:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.PlayOneShot(SoundManager.Instance.reelFishingClip, .05f);
                break;
            default:
                Debug.LogWarning("Unhandled audio clip: " + clipName);
                break;
        }
    }

    public static void PlayDialogue(AudioDialogue clipName)
    {
        switch (clipName)
        {
            case AudioDialogue.deerFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.deerFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.BookAlchemistFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.BookAlchemistFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.BookSpaceFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.BookSpaceFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Crops1Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Crops1Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Crops2Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Crops2Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Crops3Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Crops3Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.CropsDoneFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.CropsDoneFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.ChillingFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.ChillingFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Fail1Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Fail1Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Fail2Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Fail2Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Win1Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Win1Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Pond1Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Pond1Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Pond2Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Pond2Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Pond3Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Pond3Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.AutumnToothBrushFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.AutumnToothBrushFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.SpringToothFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.SpringToothFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.SummerToothFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.SummerToothFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.WinterToothFemale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.WinterToothFemale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Well1Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Well1Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Well2Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Well2Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Furnace1Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Furnace1Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Furnace2Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Furnace2Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Furnace3Female:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Furnace3Female;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.deerMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.deerMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.BookAlchemistMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.BookAlchemistMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.BookSpaceMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.BookSpaceMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Crops1Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Crops1Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Crops2Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Crops2Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Crops3Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Crops3Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.CropsDoneMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.CropsDoneMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.ChillingMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.ChillingMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Fail1Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Fail1Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Fail2Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Fail2Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Win1Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Win1Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Pond1Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Pond1Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Pond2Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Pond2Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Pond3Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Pond3Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.AutumnToothBrushMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.AutumnToothBrushMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.SpringToothMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.SpringToothMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.SummerToothMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.SummerToothMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.WinterToothMale:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.WinterToothMale;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Well1Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Well1Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Well2Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Well2Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Furnace1Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Furnace1Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Furnace2Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Furnace2Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            case AudioDialogue.Furnace3Male:
                SoundManager.Instance.ResetValues();
                SoundManager.Instance.audioSourceFX.resource = SoundManager.Instance.Furnace3Male;
                SoundManager.Instance.audioSourceFX.Play();
                break;
            default:
                Debug.LogWarning("Unhandled audio clip: " + clipName);
                break;
        }
    }

    // Reset Values before playing in case the randomizer was used before
    private void ResetValues()
    {
        audioSourceFootSteps.pitch = 1;
        audioSourceFootSteps.volume = 1;
        audioSourceFX.pitch = 1;
        audioSourceFX.volume = 0.7f;
    }

    // Randomize pitch for walking steps mainly
    private void RandomizeValues()
    {

        float randomPitch = UnityEngine.Random.Range(pitchMin, pitchMax + 0.0001f);
        audioSourceFootSteps.pitch = randomPitch;
    }
}
