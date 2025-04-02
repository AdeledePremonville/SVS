using UnityEngine;

namespace Menus
{

    public class PrefabRenderer : MonoBehaviour
    {
        public GameObject[] prefabs;
        private GameObject _instance;

        private void Start()
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.LogError("[PrefabRenderer]: Prefab array is null");
                return;
            }

            foreach (var prefab in prefabs)
            {
                _instance = Instantiate(prefab);
                _instance.layer = LayerMask.NameToLayer("RenderLayer");
                
                // Stop considering instances like players
                foreach (var script in _instance.GetComponents<MonoBehaviour>())
                {
                    Destroy(script);
                }
            }
        }
    }

}