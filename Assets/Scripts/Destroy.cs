using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Engel")
        {
            GameObject.FindObjectOfType<GameManager>().StateToBeChanged(GameStates.LevelBurning);
        }
            Destroy(other.gameObject);
    }
}
