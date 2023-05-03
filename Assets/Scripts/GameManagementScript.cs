using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class GameManagementScript : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject PlayerCube;
    // Start is called before the first frame update
    void Start()
    {
        DataHolder.PlayerCount = 1;
        for (int i = 0; i < DataHolder.PlayerCount; i++)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            var cube = Instantiate(PlayerCube, null);
            cube.transform.position = spawnPoint.transform.position;
            cube.GetComponent<PlayerMoveScript>().buttons = PlayerButtons.Buttons[i];
            spawnPoints.Remove(spawnPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
