using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Menus
{
    
    public class CharacterSelectPrefab : MonoBehaviour
    {
        public GameObject[] characters;
        
        private Controls _controls;
        private GameObject[] _characters;
        private int _selectedCharIndex = 0;
        private int _playerIndex = -1;
        
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
            }
            else
            {
                Managers.GameManager.Instance.Player2Device = input.devices[0];
            }
            
            foreach (var character in characters)
            {
                var selector1 = character.transform.Find("Selector1")?.gameObject;
                var selector2 = character.transform.Find("Selector2")?.gameObject;

                if (selector1 != null && selector2 != null)
                {
                    if (_playerIndex == 0)
                    {
                        selector1.gameObject.SetActive(false);
                        selector2.gameObject.SetActive(false);
                    } else if (_playerIndex == 1)
                        selector2.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("No Selector in character selection");
                }
            }
            
            if (characters != null && characters.Length > 0)
            {
                ToggleSelectorForPlayer(true);
            }
            else
            {
                Debug.LogWarning("No characters found in Start()");
            }
            
            StartCoroutine(EnableControlsWithDelay());
        }
        
        private void ToggleSelectorForPlayer(bool toggle)
        {
            GameObject selector;

            if (_playerIndex == 0)
            {
                selector = characters[_selectedCharIndex].transform.Find("Selector1")?.gameObject;
            }
            else
            {
                selector = characters[_selectedCharIndex].transform.Find("Selector2")?.gameObject;
            }
            
            if (selector != null)
                selector.gameObject.SetActive(toggle);
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
        
        private void OnMoveLeft()
        {
            if ((_playerIndex == 0 && Managers.GameManager.Instance.player1Ready) || (_playerIndex == 1 && Managers.GameManager.Instance.player2Ready))
                return;
            ToggleSelectorForPlayer(false);
            if (_selectedCharIndex == 0)
            {
                _selectedCharIndex = characters.Length - 1;
            }
            else if (_selectedCharIndex == characters.Length - 1)
            {
                _selectedCharIndex = 0;
            }
            else
            {
                _selectedCharIndex--;
            }
            ToggleSelectorForPlayer(true);
        }
        
        private void OnMoveRight()
        {
            if ((_playerIndex == 0 && Managers.GameManager.Instance.player1Ready) || (_playerIndex == 1 && Managers.GameManager.Instance.player2Ready))
                return;
            ToggleSelectorForPlayer(false);
            if (_selectedCharIndex == 0)
            {
                _selectedCharIndex = characters.Length - 1;
            }
            else if (_selectedCharIndex == characters.Length - 1)
            {
                _selectedCharIndex = 0;
            }
            else
            {
                _selectedCharIndex++;
            }
            ToggleSelectorForPlayer(true);
        }
        
        private void OnReady()
        {
            if (_playerIndex == 0)
            {
                Managers.GameManager.Instance.player1Ready = true;
                Managers.GameManager.Instance.player1Character = characters[_selectedCharIndex].name;
            } else
            {
                Managers.GameManager.Instance.player2Ready = true;
                Managers.GameManager.Instance.player2Character = characters[_selectedCharIndex].name;
            }

            if (Managers.GameManager.Instance.Player1Device != null &&
                Managers.GameManager.Instance.Player2Device != null)
            {
                if (Managers.GameManager.Instance.player1Ready &&
                    Managers.GameManager.Instance.player2Ready)
                {
                    SceneManager.LoadScene("MapSelect");
                }
            }
            else
            {
                SceneManager.LoadScene("MapSelect");
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
        
    }
}