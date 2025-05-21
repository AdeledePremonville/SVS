using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MapSelectorPrefab : MonoBehaviour
    {
        public GameObject[] maps;
        
        private Controls _controls;
        private int _playerIndex = -1;
        private float _inputDelay = 0.5f;
        private int _selectedCharIndex = 0;
        
        private void Start()
        {
            foreach (var map in maps)
            {
                var selector1 = map.transform.Find("Selector1")?.gameObject;
                var selector2 = map.transform.Find("Selector2")?.gameObject;

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
                    Debug.LogWarning("No Selector in map selection");
                }
            }
            
            if (maps != null && maps.Length > 0)
            {
                ToggleSelectorForPlayer(true);
            }
            else
            {
                Debug.LogWarning("No maps found in Start()");
            }
        }
        
        private void Awake()
        {
            _controls = new Controls(); 
            
            var input = GetComponent<PlayerInput>();
            if (!input) return;
            
            _controls.asset.devices = input.devices;
            _playerIndex = input.playerIndex;
            
            
            StartCoroutine(EnableControlsWithDelay());
        }
        
        private void OnDestroy()
        {
            DisableControls();
        }
        
        private void OnDisable()
        {
            DisableControls();
        }
        
        private void DisableControls()
        {
            _controls.Menu.Disable();
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
        
        private void ToggleSelectorForPlayer(bool toggle)
        {
            GameObject selector;
            
            if (_playerIndex == 0)
            {
                selector = maps[_selectedCharIndex].transform.Find("Selector1")?.gameObject;
            }
            else
            {
                selector = maps[_selectedCharIndex].transform.Find("Selector2")?.gameObject;
            }
            
            if (selector != null)
                selector.gameObject.SetActive(toggle);
        }
        
        private void OnMoveLeft()
        {
            if ((_playerIndex == 0 && Managers.GameManager.Instance.player1SelectedMap) ||
                (_playerIndex == 1 && Managers.GameManager.Instance.player2SelectedMap))
            {
                return;
            }
            ToggleSelectorForPlayer(false);
            if (_selectedCharIndex == 0)
            {
                _selectedCharIndex = maps.Length - 1;
            }
            else if (_selectedCharIndex == maps.Length - 1)
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
            if ((_playerIndex == 0 && Managers.GameManager.Instance.player1SelectedMap) || (_playerIndex == 1 && Managers.GameManager.Instance.player2SelectedMap))
                return;
            ToggleSelectorForPlayer(false);
            if (_selectedCharIndex == 0)
            {
                _selectedCharIndex = maps.Length - 1;
            }
            else if (_selectedCharIndex == maps.Length - 1)
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
                Managers.GameManager.Instance.player1SelectedMap = true;
            } else
            {
                Managers.GameManager.Instance.player2SelectedMap = true;
            }

            if (Managers.GameManager.Instance.Player1Device != null &&
                 Managers.GameManager.Instance.Player2Device != null)
            {
                if (Managers.GameManager.Instance.player1SelectedMap &&
                    Managers.GameManager.Instance.player2SelectedMap)
                {
                    Managers.GameManager.Instance.selectedMap = maps[_selectedCharIndex].name;
                    SceneManager.LoadScene("GameScene");
                }
            }
            else
            {
                Managers.GameManager.Instance.selectedMap = maps[_selectedCharIndex].name;
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}
