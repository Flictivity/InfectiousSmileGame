using System.Collections;
using UnityEngine;

public class TaggingScript : MonoBehaviour
{
    private Rigidbody2D tagBody;
    private static bool _canCollide = true;

    private void Awake()
    {
        tagBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canCollide)
        {
            return;
        }

        if (collision.gameObject.tag != "TagBox")
        {
            return;
        }

        var thisCube = gameObject.transform.parent.GetComponent<PlayerInfoScript>();
        if (!thisCube.IsTagged)
        {
            return;
        }
        if (collision.transform.parent.gameObject.GetComponent<PlayerMoveScript>().Shield)
        {
            return;
        }
        StartCoroutine(Cooldown(collision));
    }

    private IEnumerator Cooldown(Collider2D collision)
    {
        _canCollide = false;

        var thisCube = gameObject.transform.parent.GetComponent<PlayerInfoScript>();

        if (collision.gameObject.tag == "TagBox")
        {
            var enterCube = collision.transform.parent.gameObject.GetComponent<PlayerInfoScript>();
            enterCube.Smile.gameObject.SetActive(true);
            thisCube.Smile.gameObject.SetActive(false);
            thisCube.IsTagged = false;
            enterCube.IsTagged = true;
        }
        yield return new WaitForSeconds(0.5f);
        _canCollide = true;
    }
}
