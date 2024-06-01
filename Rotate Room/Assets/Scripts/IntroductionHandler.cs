using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionHandler : MonoBehaviour
{
    [SerializeField] private GameObject introduction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        introduction.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        introduction.SetActive(false);
    }
}
