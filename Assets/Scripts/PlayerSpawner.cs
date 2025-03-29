using UnityEngine;
using UnityEngine.InputSystem;

public class GameSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnPlayer(
            GameManager.Instance.player1Character,
            GameManager.Instance.player1Device,
            spawnPoints[0],
            0
        );

        SpawnPlayer(
            GameManager.Instance.player2Character,
            GameManager.Instance.player2Device,
            spawnPoints[1],
            1
        );
    }

    void SpawnPlayer(string characterName, InputDevice device, Transform spawnPoint, int playerIndex)
    {

        var player = PlayerInput.Instantiate(
            characterPrefab,
            playerIndex: playerIndex,
            controlScheme: null,
            pairWithDevice: device
        );

        player.transform.position = spawnPoint.position;
    }

    // GameObject GetPrefabByName(string name)
    // {
    //     foreach (var prefab in characterPrefabs)
    //     {
    //         if (prefab.name == name)
    //             return prefab;
    //     }
    //
    //     Debug.LogError($"Character prefab not found: {name}");
    //     return null;
    // }
}