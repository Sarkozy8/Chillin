using System.Collections;
using UnityEngine;

public class MoveStarAround : MonoBehaviour
{
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Keep the timer looping every 4 seconds
        float t = timer % 4f;

        float zRotation = 0f;

        if (t < 1f)
        {
            zRotation = Mathf.Lerp(0f, 15f, t / 1f);
        }
        else if (t < 2f)
        {
            zRotation = Mathf.Lerp(15f, 0f, (t - 1f) / 1f);
        }
        else if (t < 3f)
        {
            zRotation = Mathf.Lerp(0f, -15f, (t - 2f) / 1f);
        }
        else if (t < 4f)
        {
            zRotation = Mathf.Lerp(-15f, 0f, (t - 3f) / 1f);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }
}
