using UnityEngine;
using UnityEngine.InputSystem;

public class GameSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        // SpawnPlayer(
        //     Managers.GameManager.Instance.player1Character,
        //     Managers.GameManager.Instance.Player1Device,
        //     spawnPoints[0],
        //     0
        // );
        //
        // SpawnPlayer(
        //     Managers.GameManager.Instance.player2Character,
        //     Managers.GameManager.Instance.Player2Device,
        //     spawnPoints[1],
        //     1
        // );
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