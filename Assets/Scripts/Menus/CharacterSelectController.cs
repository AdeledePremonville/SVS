using UnityEngine;

namespace Menus
{
    public class CharacterSelectController : MonoBehaviour
    {
        public GameObject characterSelectPrefab;
        public GameObject[] characters;
        public GameObject[] playerPrefabs;
        
        private void Start()
        {
            var script = characterSelectPrefab.GetComponent<CharacterSelectPrefab>();
            
            script.characters = characters;
            script.playerPrefabs = playerPrefabs;
        }
    }
}