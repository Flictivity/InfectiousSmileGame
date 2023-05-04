using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class GameManagementScript : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject PlayerCube;
    public List<GameObject> Players;
    [SerializeField] public CameraControl CameraControl;
    // Start is called before the first frame update
    void Start()
    {
        DataHolder.PlayerCount = 4;
        for (int i = 0; i < DataHolder.PlayerCount; i++)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            var cube = Instantiate(PlayerCube, null);
            cube.transform.position = spawnPoint.transform.position;
            cube.GetComponent<PlayerMoveScript>().buttons = PlayerSettings.Buttons[i];
            cube.GetComponent<SpriteRenderer>().material.color = PlayerSettings.Colors[i];
            Players.Add(cube);
            spawnPoints.Remove(spawnPoint);
            CameraControl.Targets.Add(cube.transform);
        }
        var taggedPlayer = Players[Random.Range(0, Players.Count)].GetComponent<PlayerInfoScript>();
        taggedPlayer.IsTagged = true;
        taggedPlayer.Smile.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
