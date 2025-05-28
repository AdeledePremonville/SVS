using UnityEngine;

namespace Menus
{
    public class CharacterSelectController : MonoBehaviour
    {
        public GameObject[] characters;
        public GameObject characterSelectPrefab;
        
        private void Start()
        {
            var script = characterSelectPrefab.GetComponent<CharacterSelectPrefab>();
            script.characters = characters;
        }
    }
}