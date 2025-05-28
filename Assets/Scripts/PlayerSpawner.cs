using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] playerPrefabs;
    public GameObject roundManager;

    private void Start()
    {
        if (GameManager.Instance.Player1Device != null)
        {
            // Player1 Spawning
            var player = SpawnPlayer(
                GameManager.Instance.player1Character,
                GameManager.Instance.Player1Device,
                spawnPoints[0],
                0
            );
            roundManager.GetComponent<RoundManager>().player1 = player;
        }
        else
        {
            Debug.LogWarning("Cannot find player 1");
        }
        if (GameManager.Instance.Player2Device != null)
        {
            // Player2 Spawning
            var player = SpawnPlayer(
                GameManager.Instance.player2Character,
                GameManager.Instance.Player2Device,
                spawnPoints[1],
                1
            );
            roundManager.GetComponent<RoundManager>().player2 = player;
        }
        else
        {
            // AI Spawning
            var player = SpawnPlayer(
                GameManager.Instance.player1Character,
                GameManager.Instance.Player2Device,
                spawnPoints[1],
                1
            );
            roundManager.GetComponent<RoundManager>().player2 = player;
        }
    }

    private GameObject GetPrefabByName(string prefabName)
    {
        foreach (var prefab in playerPrefabs)
        {
            if (prefab.name == prefabName)
                return prefab;
        }
    
        Debug.LogError($"Character prefab not found: {prefabName}");
        return null;
    }
    
    private GameObject SpawnPlayer(string characterPrefab, InputDevice device, Transform spawnPoint, int playerIndex)
    {
        string controlScheme = null;
        
        if (device == null)
        {
            controlScheme = "AI";
        }

        var player = PlayerInput.Instantiate(
            GetPrefabByName(characterPrefab),
            playerIndex: playerIndex,
            controlScheme: controlScheme,
            pairWithDevice: device
        );


        player.transform.position = spawnPoint.position;
        return player.gameObject;
    }

}