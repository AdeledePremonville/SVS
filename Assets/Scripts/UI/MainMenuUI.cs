using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    
    public class MainMenuUI : MonoBehaviour
    {
        public void OnVersusModeClick()
        {
            GameManager.Instance.gameMode = "Versus";
            SceneManager.LoadScene("CharacterSelectScene");
        }

        public void OnStoryModeClick()
        {
            GameManager.Instance.gameMode = "Story";
            // SceneManager.LoadScene("CharacterSelectScene");
        }
    }
    
}

