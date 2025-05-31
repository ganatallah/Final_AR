using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public float delay = 0.3f;

    public void LoadMarkerlessScene()
    {
        StartCoroutine(LoadWithDelay("MarkerlessScene"));
    }

    public void LoadMarkerBasedScene()
    {
        StartCoroutine(LoadWithDelay("MarkerBasedScene"));
    }

    private IEnumerator LoadWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
