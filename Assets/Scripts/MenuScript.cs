using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void SelectPlayersCount(int count)
    {
        DataHolder.PlayerCount = count;
    }

    public void SelectLevel(int level)
    {
        DataHolder.Level = level;
        SceneManager.LoadScene("SampleScene");
    }
}
