using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Menus
{
    
    public class CharacterSelectController : MonoBehaviour
    {
        public TextMeshProUGUI player1ReadyText;
        public TextMeshProUGUI player2ReadyText;
        
        private Controls _controls;
        private GameObject[] _characters;
        private int _selectedCharIndex = 0;
        private int _playerIndex = -1;
        private string _selectorQuad;
        
        private float _inputDelay = 0.5f;
        
        private void Awake()
        {
            _controls = new Controls(); 
            
            var input = GetComponent<PlayerInput>();
            if (!input) return;
            
            _controls.asset.devices = input.devices;
            _playerIndex = input.playerIndex;

            if (_playerIndex == 0)
            {
                Managers.GameManager.Instance.Player1Device = input.devices[0];
                _selectorQuad = "SelectorQuad1";
            }
            else
            {
                _selectorQuad = "SelectorQuad2";
                Managers.GameManager.Instance.Player2Device = input.devices[0];
            }
            SpawnSelector();
            
            StartCoroutine(EnableControlsWithDelay());
        }
        
        private IEnumerator EnableControlsWithDelay()
        {
            _controls.Menu.Disable();
            yield return new WaitForSeconds(_inputDelay);
            _controls.Menu.Enable();
            
            _controls.Menu.MoveLeft.performed += _ => OnMoveLeft();
            _controls.Menu.MoveRight.performed += _ => OnMoveRight();
            _controls.Menu.Ready.performed += _ => OnReady();
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
                var selector = _characters[_selectedCharIndex].transform.Find(_selectorQuad);
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

            var nextSelector = _characters[_selectedCharIndex]?.transform.Find(_selectorQuad);
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
            if ((_playerIndex == 0 && Managers.GameManager.Instance.player1Ready) ||
                (_playerIndex == 1 && Managers.GameManager.Instance.player2Ready))
                return;
            
            UpdateSelector(-1);
        }

        private void OnMoveRight()
        {
            if ((_playerIndex == 0 && Managers.GameManager.Instance.player1Ready) ||
                (_playerIndex == 1 && Managers.GameManager.Instance.player2Ready))
                return;
            
            UpdateSelector(1);
        }

        private void OnReady()
        {
            if ((_playerIndex == 0 && Managers.GameManager.Instance.player1Ready) ||
                (_playerIndex == 1 && Managers.GameManager.Instance.player2Ready))
                return;
            
            if (_characters[_selectedCharIndex] == null)
                Debug.LogError("_characters[_selectedCharIndex] is null");
            
            if (_playerIndex == 0)
            {
                Managers.GameManager.Instance.player1Character = _characters[_selectedCharIndex].name;
                Managers.GameManager.Instance.player1Ready = true;
                player1ReadyText.text = "Ready";
            }
            else
            {
                Managers.GameManager.Instance.player2Character = _characters[_selectedCharIndex].name;
                Managers.GameManager.Instance.player2Ready = true;
                player2ReadyText.text = "Ready";
            }

            if ((Managers.GameManager.Instance.Player1Device != null && Managers.GameManager.Instance.Player2Device == null) ||
                (Managers.GameManager.Instance.Player2Device != null && Managers.GameManager.Instance.Player1Device == null) ||
                Managers.GameManager.Instance.player1Ready && Managers.GameManager.Instance.player2Ready)
            {
                SceneManager.LoadScene("MapSelect");
            }
        }
        
    }
}