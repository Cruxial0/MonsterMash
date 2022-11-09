using System.Linq;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryButton : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    private void Start()
    {
        //Add onClick Listener
        button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        //Load Scene of name "Level0"
        SceneManager.LoadScene(LevelManager.GetAllScenes().First(x => x.LevelID == 0).Level.SceneName);
    }
}