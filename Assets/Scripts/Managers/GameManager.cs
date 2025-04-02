using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public string player1Character;
        public string player2Character;
        public InputDevice Player1Device;
        public InputDevice Player2Device;
        public bool player2Ready = false;
        public bool player1Ready = false;

        public string gameMode; // "Solo", "Versus"

        private void Awake()
        {
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

