using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;


    public void PlayEffect()
    {
        gameManager.BoxRippedEffect(transform.position);
        gameObject.SetActive(false);
    }


}
