using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowroomHandler : MonoBehaviour
{
    [SerializeField] private FollowPlayer camFollow;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        camFollow.SetLimits(boxCollider.offset.y + boxCollider.size.y / 2f, boxCollider.offset.x + boxCollider.size.x / 2f,
                            boxCollider.offset.y - boxCollider.size.y / 2f, boxCollider.offset.x - boxCollider.size.x / 2f);
        if (gameManager != null) gameManager.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        if (boxCollider.offset.y - boxCollider.size.y / 2f <= collision.transform.position.y
          && boxCollider.offset.y + boxCollider.size.y / 2f >= collision.transform.position.y)
        {
            return;
        }
        if (gameManager != null) gameManager.SetActive(true);
    }
}
