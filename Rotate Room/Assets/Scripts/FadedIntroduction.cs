using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadedIntroduction : MonoBehaviour
{
    [SerializeField] private GameObject introduction;
    [SerializeField] private Collider2D coll;
    private void Update()
    {
        introduction.SetActive(coll.enabled);
    }
}
