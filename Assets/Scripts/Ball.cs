using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private int number;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ParticleSystem mergeEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isBallDefault;

    private bool first;

    void Start()
    {
        numberText.text = number.ToString();

        if (isBallDefault)
            first = true;

    }


    private void ArrangeStatus()
    {
        first = true;
    }

    public void ChangeFirstStatus()
    {
        Invoke("ArrangeStatus", 2f);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(number.ToString()) && first)
        {
            mergeEffect.Play();
            collision.gameObject.SetActive(false);
            number += number;
            gameObject.tag = number.ToString();
            numberText.text = number.ToString();

            switch (number)
            {
                case 4:
                    spriteRenderer.sprite = gameManager.spriteObjects[1];
                    break;
                case 8:
                    spriteRenderer.sprite = gameManager.spriteObjects[2];
                    break;
                case 16:
                    spriteRenderer.sprite = gameManager.spriteObjects[3];
                    break;
                case 32:
                    spriteRenderer.sprite = gameManager.spriteObjects[4];
                    break;
                case 64:
                    spriteRenderer.sprite = gameManager.spriteObjects[5];
                    break;
                case 128:
                    spriteRenderer.sprite = gameManager.spriteObjects[6];
                    break;
                case 256:
                    spriteRenderer.sprite = gameManager.spriteObjects[7];
                    break;
                case 512:
                case 1024:
                case 2048:
                    spriteRenderer.sprite = gameManager.spriteObjects[8];
                    break;
                
            }
            if (gameManager.isBallTargetActive)
            {
                gameManager.MissionCountControl(number);
            }
            first = false;
            Invoke("ArrangeStatus", 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(number.ToString()) && first)
        {
            mergeEffect.Play();
            collision.gameObject.SetActive(false);
            number += number;
            gameObject.tag = number.ToString();
            numberText.text = number.ToString();

            switch (number)
            {
                case 4:
                    spriteRenderer.sprite = gameManager.spriteObjects[1];
                    break;
                case 8:
                    spriteRenderer.sprite = gameManager.spriteObjects[2];
                    break;
                case 16:
                    spriteRenderer.sprite = gameManager.spriteObjects[3];
                    break;
                case 32:
                    spriteRenderer.sprite = gameManager.spriteObjects[4];
                    break;
                case 64:
                    spriteRenderer.sprite = gameManager.spriteObjects[5];
                    break;
                case 128:
                    spriteRenderer.sprite = gameManager.spriteObjects[6];
                    break;
                case 256:
                    spriteRenderer.sprite = gameManager.spriteObjects[7];
                    break;
                case 512:
                case 1024:
                case 2048:
                    spriteRenderer.sprite = gameManager.spriteObjects[8];
                    break;

            }

            if (gameManager.isBallTargetActive)
            {
                gameManager.MissionCountControl(number);
            }

            first = false;
            Invoke("ArrangeStatus", 2f);
        }
    }

}
