using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class SceneManagement : MonoBehaviour
{
    public enum TrainScenes { BeforeMurder, AfterMurder, Night1, Night2}
    public TrainScenes currentScene = TrainScenes.BeforeMurder;

    private int sceneToLoadIndex;

    public Animator transitionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSceneToLoad()
    {
        switch (currentScene)
        {
            case TrainScenes.BeforeMurder:
                sceneToLoadIndex = 1;
                break;

            case TrainScenes.AfterMurder:
                sceneToLoadIndex = 2;
                break;

            case TrainScenes.Night1:
                sceneToLoadIndex = 3;
                break;
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        SetSceneToLoad();

        transitionCanvas.Play("Fade Out");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneToLoadIndex);

        yield return new WaitForSeconds(1);

        transitionCanvas.Play("Fade In");
    }
}
