using UnityEngine;
using UnityEngine.InputSystem;

namespace Menus
{
    public class MapSelectController : MonoBehaviour
    {
        public GameObject playerPrefab;
        public GameObject[] maps;
        
        private void Start()
        {
            var script = playerPrefab.GetComponent<MapSelectorPrefab>();
            script.maps = maps;
            
            if (Managers.GameManager.Instance.Player1Device != null)
            {
                SpawnPlayer(
                    Managers.GameManager.Instance.Player1Device,
                    0
                );
            }
            if (Managers.GameManager.Instance.Player2Device != null)
            {
                SpawnPlayer(
                    Managers.GameManager.Instance.Player2Device,
                    1
                );
            }
        }
        
        private void SpawnPlayer(InputDevice device, int playerIndex)
        {
            PlayerInput.Instantiate(
                playerPrefab,
                playerIndex: playerIndex,
                controlScheme: null,
                pairWithDevice: device
            );
        }
    }
}