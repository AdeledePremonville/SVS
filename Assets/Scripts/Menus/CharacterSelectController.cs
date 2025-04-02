using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class CharacterSelectController : MonoBehaviour
    {
        private Controls _controls;
        private GameObject[] _characters;
        private int _selectedCharIndex = 0;
        private int _playerIndex = -1;

        private void Awake()
        {
            _controls = new Controls();

            _controls.Menu.MoveLeft.performed += _ => OnMoveLeft();
            _controls.Menu.MoveRight.performed += _ => OnMoveRight();
            _controls.Menu.Ready.performed += _ => OnReady();
            
            var input = GetComponent<PlayerInput>();
            if (!input) return;
            
            _playerIndex = input.playerIndex;

            if (_playerIndex == 0)
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
            if (_characters.Length < 1)
            {
                Debug.LogError("[CharacterSelectController]: No characters found");
                return;
            }
            
            UpdateSelector();
        }

        private static void DisableSelector(MeshRenderer selector)
        {
            if (selector != null)
            {
                selector.enabled = false;
            }
            else
            {
                Debug.LogWarning("[CharacterSelectController]: Unable to find selector quad or mesh controller !");
            }
        }

        private static void EnableSelector(MeshRenderer selector)
        {
            if (selector != null)
            {
                selector.enabled = true;
            }
            else
            {
                Debug.LogWarning("[CharacterSelectController]: Unable to find selector quad or mesh controller !");
            }
        }

        private void UpdateSelector(int increment = 0)
        {
            if (_characters is { Length: > 0 } && _characters[_selectedCharIndex] != null)
            {
                var selector = _characters[_selectedCharIndex].transform.Find("SelectorQuad");
                if (selector != null)
                {
                    DisableSelector(selector.GetComponent<MeshRenderer>());
                }
                else
                {
                    Debug.LogWarning($"[CharacterSelectController]: SelectorQuad not found on character at index {_selectedCharIndex}");
                }
            }

            if (_characters == null || _characters.Length == 0)
                return;

            if (_selectedCharIndex + increment >= _characters.Length)
            {
                _selectedCharIndex = 0;
            }
            else if (_selectedCharIndex + increment < 0)
            {
                _selectedCharIndex = _characters.Length - 1;
            }
            else
            {
                _selectedCharIndex += increment;
            }

            var nextSelector = _characters[_selectedCharIndex]?.transform.Find("SelectorQuad");
            if (nextSelector != null)
            {
                EnableSelector(nextSelector.GetComponent<MeshRenderer>());
            }
            else
            {
                Debug.LogWarning($"[CharacterSelectController]: SelectorQuad not found on new character at index {_selectedCharIndex}");
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

        private void OnMoveLeft()
        {
            UpdateSelector(-1);
        }

        private void OnMoveRight()
        {
            UpdateSelector(1);
        }

        private void OnReady()
        {
            if (_characters[_selectedCharIndex] == null)
                Debug.LogError("_characters[_selectedCharIndex] is null");
            
            if (_playerIndex == 0)
            {
                Managers.GameManager.Instance.player1Character = _characters[_selectedCharIndex].name;
            }
            else
            {
                Managers.GameManager.Instance.player2Character = _characters[_selectedCharIndex].name;
            }
            
            SceneManager.LoadScene("GameScene");
        }
        
    }
}