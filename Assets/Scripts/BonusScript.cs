using Assets.Scripts;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    [SerializeField] public BonusType Type;
    [SerializeField] public float Duration;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<PlayerMoveScript>().IsBonusApply)
        {
            var bonus = new Bonus
            {
                Type = Type,
                Duration = Duration
            };
            collision.gameObject.GetComponent<PlayerMoveScript>().Bonus = bonus;
            Destroy(gameObject);
        }
    }
}
