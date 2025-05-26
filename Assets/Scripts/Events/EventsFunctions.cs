using TMPro;
using UnityEngine;

public class EventsFunctions : MonoBehaviour
{
    /// <summary>
    /// For the count down for brushing, this are the events use in the animator to count down the numbers.
    /// </summary>

    public TextMeshProUGUI mainText; // Text on the middle, used for countdowns and telling you how you did in the minigame

    // Accumulation of functions to use on animations!

    public void count3()
    {
        mainText.text = "3";
    }
    public void count2()
    {
        mainText.text = "2";
    }
    public void count1()
    {
        mainText.text = "1";
    }
    public void countGo()
    {
        mainText.text = "Go!";
    }
}
