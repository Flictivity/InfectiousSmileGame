using UnityEngine;

public class TaggingScript : MonoBehaviour
{
    private Rigidbody2D tagBody;

    private void Awake()
    {
        tagBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var thisCube = gameObject.transform.parent.GetComponent<PlayerMoveScript>();
        //if (!thisCube.IsTagged)
        //{
        //    return;
        //}
        if (collision.gameObject.tag == "TagBox")
        {
            var enteredCube = collision.gameObject.transform.parent.GetComponent<PlayerMoveScript>();

            enteredCube.IsTagged = true;
            thisCube.IsTagged = false;

            thisCube.Smile.gameObject.SetActive(false);
            enteredCube.Smile.gameObject.SetActive(true);
            Debug.Log(123);
        }
    }
}
