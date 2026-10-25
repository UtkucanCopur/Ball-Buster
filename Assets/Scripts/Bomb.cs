using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private int number;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private GameManager gameManager;

    private List<Collider2D> colliders = new List<Collider2D>();



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(number.ToString()))
            AddForce();
            

    }


    private void AddForce()
    {
        var contactFilter2D = new ContactFilter2D
        {
            useTriggers = true
        };


        Physics2D.OverlapBox(transform.position, transform.localScale * 4, 20f, contactFilter2D,colliders);

        gameManager.ExplosionEffect(transform.position);
        gameObject.SetActive(false);

        foreach (var item in colliders)
        {
            item.gameObject.GetComponent<Rigidbody2D>().AddForce(90 * new Vector2(0, 6), ForceMode2D.Force);
        }

    }


}
