using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{
    public TextMeshProUGUI label;
    public RectTransform labelRect;
    
    void Start()
    {
        var input = GetComponent<PlayerInput>();
        int index = input.playerIndex;

        label.text = "Player " + (index + 1);

        if (index == 0) {
            labelRect.anchorMin = new Vector2(0, 0.5f);
            labelRect.anchorMax = new Vector2(0, 0.5f);
            labelRect.anchoredPosition = new Vector2(100f, 0f);
            labelRect.pivot = new Vector2(0, 0.5f);
            GameManager.Instance.player1Device = input.devices[0];
        } else {
            labelRect.anchorMin = new Vector2(1, 0.5f);
            labelRect.anchorMax = new Vector2(1, 0.5f);
            labelRect.anchoredPosition = new Vector2(-100f, 0f);
            labelRect.pivot = new Vector2(1, 0.5f);
            GameManager.Instance.player2Device = input.devices[0];
        }
        
        OnReady();
    }
    
    void OnReady()
    {
        label.text += "\nReady!";
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}