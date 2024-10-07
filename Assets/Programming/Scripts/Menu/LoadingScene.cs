using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadingScenePanel;
    public Image progressBar;
    public Text progressText;

    IEnumerator LoadASynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScenePanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.fillAmount = progress;
            progressText.text = $"{progress * 100:P2}";
            yield return null;
        }
        yield return null;
    }
    public void LoadLevelAsync(int sceneIndex)
    {
        StartCoroutine(LoadASynchronously(sceneIndex));
    }

}
