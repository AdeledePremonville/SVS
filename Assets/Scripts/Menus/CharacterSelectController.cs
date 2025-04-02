using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class CharacterSelectController : MonoBehaviour
    {
        public GameObject selectorPrefab;
        
        private Controls _controls;
        private GameObject[] _characters;

        private void Awake()
        {
            _controls = new Controls();

            _controls.Menu.MoveLeft.performed += _ => OnMoveLeft();
            _controls.Menu.MoveRight.performed += _ => OnMoveRight();
            
            var input = GetComponent<PlayerInput>();
            if (!input) return;

            if (input.playerIndex == 0)
            {
                Managers.GameManager.Instance.Player1Device = input.devices[0];
                SpawnSelector();
            } else
                Managers.GameManager.Instance.Player2Device = input.devices[0];
        }

        private void SpawnSelector()
        {
            if (_characters == null || _characters.Length == 0)
            {
                _characters = GameObject.FindGameObjectsWithTag("Player");
            }

            if (_characters.Length >= 1)
            {
                var position = new Vector3(_characters[0].transform.position.x, _characters[0].transform.position.y, _characters[0].transform.position.z);
                Instantiate(selectorPrefab, position, Quaternion.identity);
                Debug.Log(_characters[0].transform.position);
            }
        }

        private void OnEnable()
        {
            _controls.Menu.Enable();
        }

        private void OnDisable()
        {
            _controls.Menu.Disable();
        }

        private static void OnMoveLeft()
        {
            Debug.Log("Move Left (A)");
        }

        private static void OnMoveRight()
        {
            Debug.Log("Move Right (D)");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}