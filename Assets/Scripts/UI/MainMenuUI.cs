using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    
    public class MainMenuUI : MonoBehaviour
    {
        public void OnVersusModeClick()
        {
            // Managers.GameManager.Instance.gameMode = "Versus";
            SceneManager.LoadScene("CharacterSelectScene");
        }

        public void OnStoryModeClick()
        {
            // Managers.GameManager.Instance.gameMode = "Story";
            // SceneManager.LoadScene("CharacterSelectScene");
        }
    }
    
}

