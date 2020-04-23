using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime : MonoBehaviour
{

    [SerializeField]
    private float delayBeforeLoading = 36f;

    [SerializeField]
    private string sceneNameToLoad;

    private float timeElapsed;


    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }

        if (Input.anyKey)
        {
           // SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
