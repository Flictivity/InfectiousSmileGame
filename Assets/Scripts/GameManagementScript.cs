using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManagementScript : MonoBehaviour
{
    [SerializeField] private List<Transform> Levels;
    [SerializeField] private GameObject[] PlayerCube;
    [SerializeField] private GameObject[] BonusesPrefabs;
    [SerializeField] public CameraControl CameraControl;
    [SerializeField] private List<Transform> bonusPoints;
    [SerializeField] private Text timerText;
    [SerializeField] private Animator winDialog;
    [SerializeField] private GameObject winner;
    public List<GameObject> Players;
    private int playersCount = 0;
    private float gameTimer = 0f;
    private float bonusTimer = 0f;
    private float gameTime = 31f;
    private float bonusTime = 7f;
    private bool isGameOver;

    void Start()
    {
        bonusPoints = Levels[DataHolder.Level].GetComponent<LevelInfoScript>().bonusSpawnPoints;
        playersCount = DataHolder.PlayerCount;

        Levels[DataHolder.Level].gameObject.SetActive(true);

        var levelInfo = Levels[DataHolder.Level].GetComponent<LevelInfoScript>();

        var spawnPointsClone = new List<Transform>(levelInfo.spawnPoints);

        for (int i = 0; i < playersCount; i++)
        {
            var n = Random.Range(0, spawnPointsClone.Count);
            var spawnPoint = spawnPointsClone[n];

            var cube = Instantiate(PlayerCube[i], null);
            cube.transform.position = spawnPoint.transform.position;

            var respawnPoint = new GameObject().transform;
            respawnPoint.transform.position = spawnPoint.position;

            cube.GetComponent<PlayerMoveScript>().RespawnPoint = respawnPoint;
            cube.GetComponent<PlayerMoveScript>().buttons = PlayerSettings.Buttons[i];

            Players.Add(cube);
            spawnPointsClone.Remove(spawnPoint);
            CameraControl.Targets.Add(cube.transform);
        }

        foreach (var player in Players)
        {
            player.GetComponent<PlayerMoveScript>().Players = Players;
        }

        SetTaggedPlayer();
        gameTimer = gameTime;
        bonusTimer = bonusTime;
        SetTimerText();
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        gameTimer -= Time.deltaTime;
        SetTimerText();

        if ((int)gameTimer <= 0)
        {
            var destroyPlayer = Players.FirstOrDefault(x => x.GetComponent<PlayerInfoScript>().IsTagged);

            if (destroyPlayer == null)
            {
                return;
            }

            CameraControl.Targets.Remove(destroyPlayer.transform);
            Players.Remove(destroyPlayer);
            Destroy(destroyPlayer);
            playersCount--;

            if (playersCount == 1)
            {
                isGameOver = true;
                winDialog.SetTrigger("ChangeState");
                //Time.timeScale = 0;
                return;
            }

            RespawnPlayers();
            SetTaggedPlayer();
            gameTimer = gameTime;
        }

        bonusTimer -= Time.deltaTime;
        if ((int)bonusTimer <= 0)
        {
            var bonus = Instantiate(BonusesPrefabs[Random.Range(0, BonusesPrefabs.Length)], null);
            bonus.transform.position = bonusPoints[Random.Range(0, bonusPoints.Count)].position;
            bonusTimer = bonusTime;
        }
    }

    private void SetTimerText()
    {
        timerText.text = ((int)gameTimer).ToString();
    }

    private void SetTaggedPlayer()
    {
        var taggedPlayer = Players[Random.Range(0, Players.Count)].GetComponent<PlayerInfoScript>();
        taggedPlayer.IsTagged = true;
        taggedPlayer.Smile.gameObject.SetActive(true);
    }

    private void RespawnPlayers()
    {
        foreach (var player in Players)
        {
            var spawnPoint = player.GetComponent<PlayerMoveScript>().RespawnPoint;
            player.transform.position = spawnPoint.transform.position;
        }
    }

}
