using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Missions
{
    public Sprite targetImage;
    public int ballNumber;
    public GameObject isCompleted;
    public string MissionType;
}

[Serializable]
public class MissionsUI
{
    public GameObject target;
    public Image targetImage;
    public TextMeshProUGUI targetValueText;
    public GameObject isCompleted;
}


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



    [Header("----Missions----")]
    [SerializeField] private List<Missions> Missions;
    [SerializeField] private List<MissionsUI> MissionsUI;
    private int ballValue;
    private int boxValue;
    private bool isBoxTargetActive;
    public bool isBallTargetActive;
    private int totalMissionCount;


    [Header("----Buttons----")]
    [SerializeField] private GameObject overallPanel;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        overallPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        remainBallCount = balls.Length;
        GetBall(true);
        totalMissionCount = Missions.Count;

        for (int i = 0; i < Missions.Count; i++)
        {
            MissionsUI[i].target.SetActive(true);
            MissionsUI[i].targetImage.sprite = Missions[i].targetImage;
            MissionsUI[i].targetValueText.text = Missions[i].ballNumber.ToString();
            
            if (Missions[i].MissionType == "Ball")
            {
                isBallTargetActive = true;
                ballValue = Missions[i].ballNumber;
            } else if (Missions[i].MissionType == "Box")
            {
                isBoxTargetActive = true;
                boxValue = Missions[i].ballNumber;
            }

        }

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
        if (remainBallCount <= 0) return;
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
                

                if (poolIndex != balls.Length - 1)
                {
                    poolIndex++;
                    balls[poolIndex].transform.position = nextBall.transform.position;
                    balls[poolIndex].SetActive(true);

                }


            }

            if (remainBallCount == 0)
                Invoke("CheckMissions", 3f);

        }




    }

    private void CheckMissions()
    {
        if (totalMissionCount == 0)
            YouWin();
        else
            YouLose();
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

        if (isBoxTargetActive)
        {
            boxValue--;
            if (boxValue == 0)
            {
                MissionsUI[1].isCompleted.SetActive(true);
                totalMissionCount--;

                if (totalMissionCount == 0)
                {
                    YouWin();
                }
            }
            


        }


        if (boxRippedIndex == boxRippedEffects.Length - 1)
        {
            boxRippedIndex = 0;
        }
        else
            boxRippedIndex++;

    }

    
    public void MissionCountControl(int number)
    {
        if (number == ballValue)
        {
            MissionsUI[0].isCompleted.SetActive(true);

            totalMissionCount--;

            if (totalMissionCount == 0)
            {
                YouWin();
            }

        }
    }

    private void YouWin()
    {
        overallPanel.GetComponent<Image>().color = new Color(255, 255, 255, 121);
        nextButton.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);

    }

    private void YouLose()
    {
        overallPanel.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
