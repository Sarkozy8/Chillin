using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEnd : MonoBehaviour
{

    public void GoBackToTheMainMenu()
    {
        SceneManager.LoadScene(6);
    }
}
