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
            var player = SpawnPlayer(
                Managers.GameManager.Instance.player1Character,
                Managers.GameManager.Instance.Player1Device,
                spawnPoints[0],
                0
            );
            Managers.GameManager.Instance.player1 = player;
        }
        if (Managers.GameManager.Instance.Player2Device != null)
        {
            var player = SpawnPlayer(
                Managers.GameManager.Instance.player2Character,
                Managers.GameManager.Instance.Player2Device,
                spawnPoints[1],
                1
            );
            Managers.GameManager.Instance.player2 = player;
        }
        // else
        // {
        //     SpawnPlayer(
        //         Managers.GameManager.Instance.player1Character,
        //         Managers.GameManager.Instance.Player2Device,
        //         spawnPoints[1],
        //         1
        //     );
        // }
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
    
    private GameObject SpawnPlayer(string characterPrefab, InputDevice device, Transform spawnPoint, int playerIndex)
    {
        PlayerInput player;

        // if (device == null)
        // {
        //    player = PlayerInput.Instantiate(
        //         GetPrefabByName(characterPrefab),
        //         playerIndex: playerIndex,
        //         controlScheme: null
        //     );
        // }
        // else
        // {
            player = PlayerInput.Instantiate(
                GetPrefabByName(characterPrefab),
                playerIndex: playerIndex,
                controlScheme: null,
                pairWithDevice: device
            );
        // }


        player.transform.position = spawnPoint.position;
        return player.gameObject;
    }

}