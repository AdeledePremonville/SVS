using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Managers
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        // public GameObject player1;
        // public GameObject player2;


        public InputDevice Player1Device;
        public InputDevice Player2Device;
        public bool player2Ready = false;
        public bool player1Ready = false;
        public bool player1SelectedMap = false;
        public bool player2SelectedMap = false;
        
        public string selectedMap = "";
        public string player1Character;
        public string player2Character;

        public string gameMode; // "Solo", "Versus"


        private void Awake()
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (Instance == null)
            {
                Instance = this;
                
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    
}

