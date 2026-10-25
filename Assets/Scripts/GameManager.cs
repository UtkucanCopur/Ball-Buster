using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("----Level Settings----")]
    public Sprite[] spriteObjects;
    [SerializeField] private GameObject[] balls;
    [SerializeField] private TextMeshProUGUI remainBallCountText;
    private int remainBallCount;
    private int poolIndex;

    [Header("----Other Settings----")]
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private ParticleSystem[] boxRippedEffects;
    private int boxRippedIndex;


    [Header("----Ball Throw System----")]
    [SerializeField] private GameObject ballThrower;
    [SerializeField] private GameObject ballPlaceHolder;
    [SerializeField] private GameObject nextBall;
    [SerializeField] private GameObject currentBall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainBallCount = balls.Length;
        GetBall(true);
    }

    // 2-Red
    // 4-Yellow
    // 8-Green
    // 16-Blue
    // 32-Dark Blue
    // 64-Dark Green
    // 128-Purple
    // 256-Orange
    // 512-Mixed Yellow
    // 1024-
    // 2048-

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    ballThrower.transform.position = Vector2.MoveTowards(ballThrower.transform.position
                        , new Vector2(mousePos.x, ballThrower.transform.position.y)
                        , 30 * Time.deltaTime);
                }
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            currentBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            currentBall.transform.parent = null;
            currentBall.GetComponent<Ball>().ChangeFirstStatus();
            GetBall(false);
        }


    }

    private void GetBall(bool firstStep)
    {

        if (firstStep)
        {
            balls[poolIndex].transform.SetParent(ballThrower.transform);
            balls[poolIndex].transform.position = ballPlaceHolder.transform.position;
            balls[poolIndex].SetActive(true);
            currentBall = balls[poolIndex];
            poolIndex++;
            balls[poolIndex].transform.position = nextBall.transform.position;
            balls[poolIndex].SetActive(true);
            remainBallCountText.text = remainBallCount.ToString();

        }
        else
        {
            if (balls.Length != 0)
            {

                balls[poolIndex].transform.SetParent(ballThrower.transform);
                balls[poolIndex].transform.position = ballPlaceHolder.transform.position;
                balls[poolIndex].SetActive(true);
                currentBall = balls[poolIndex];
                remainBallCount--;
                remainBallCountText.text = remainBallCount.ToString();


                if (poolIndex == balls.Length)
                {

                }
                else
                {
                    poolIndex++;
                    balls[poolIndex].transform.position = nextBall.transform.position;
                    balls[poolIndex].SetActive(true);
                }



            }
        }




    }

    public void ExplosionEffect(Vector2 Position)
    {
        explosionEffect.gameObject.transform.position = Position;
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.Play();
    }

    public void BoxRippedEffect(Vector2 Position)
    {
        
        boxRippedEffects[boxRippedIndex].gameObject.transform.position = Position;
        boxRippedEffects[boxRippedIndex].gameObject.SetActive(true);
        boxRippedEffects[boxRippedIndex].Play();
        boxRippedIndex++;


        if (boxRippedIndex == boxRippedEffects.Length - 1)
        {
            boxRippedIndex = 0;
        }
        else
            boxRippedIndex++;

    }

}
