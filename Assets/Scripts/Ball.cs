using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private int number;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ParticleSystem mergeEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;


    void Start()
    {
        numberText.text = number.ToString();
    }

    void Update()
    {
        
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(number.ToString()))
        {
            mergeEffect.Play();
            collision.gameObject.SetActive(false);
            number += number;
            gameObject.tag = number.ToString();
            numberText.text = number.ToString();

        }
    }


}
