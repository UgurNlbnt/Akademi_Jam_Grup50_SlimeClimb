using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{       
    public Button playButton;
    public string gameSceneName = "SampleScene";

    void Start()
    {
        playButton.onClick.AddListener(() => {
            TransitionManager.Instance.LoadLevel(gameSceneName);
        });
    }

    public void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
