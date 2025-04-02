using UnityEngine;
using UnityEngine.InputSystem;

public class GameSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] playerPrefabs;

    private void Start()
    {
        if (Managers.GameManager.Instance.Player1Device != null)
        {
            SpawnPlayer(
                Managers.GameManager.Instance.player1Character,
                Managers.GameManager.Instance.Player1Device,
                spawnPoints[0],
                0
            );
        }
        if (Managers.GameManager.Instance.Player2Device != null)
        {
            SpawnPlayer(
                Managers.GameManager.Instance.player2Character,
                Managers.GameManager.Instance.Player2Device,
                spawnPoints[1],
                1
            );
        }
    }

    private GameObject GetPrefabByName(string prefabName)
    {
        if (prefabName.EndsWith("(Clone)"))
        {
            prefabName = prefabName.Substring(0, prefabName.Length - "(Clone)".Length);
        }
        
        foreach (var prefab in playerPrefabs)
        {
            if (prefab.name == prefabName)
                return prefab;
        }
    
        Debug.LogError($"Character prefab not found: {prefabName}");
        return null;
    }
    
    private void SpawnPlayer(string characterPrefab, InputDevice device, Transform spawnPoint, int playerIndex)
    {

        var player = PlayerInput.Instantiate(
            GetPrefabByName(characterPrefab),
            playerIndex: playerIndex,
            controlScheme: null,
            pairWithDevice: device
        );

        player.transform.position = spawnPoint.position;
    }

}