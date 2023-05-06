using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Cubes;
    public void SelectPlayersCount(int count)
    {
        DataHolder.PlayerCount = count;
    }

    public void SelectLevel(int level)
    {
        DataHolder.Level = level;
        SceneManager.LoadScene("SampleScene");
    }

    private void Start()
    {
        StartCoroutine(AnimateCubes());
    }

    private IEnumerator AnimateCubes()
    {
        while (true)
        {
            foreach (var cub in Cubes)
            {
                var body = cub.GetComponent<Rigidbody2D>();
                body.AddForce(new Vector2(0, Random.Range(600f, 1000f)));
            }
            yield return new WaitForSeconds(2);
        }
    }
}
