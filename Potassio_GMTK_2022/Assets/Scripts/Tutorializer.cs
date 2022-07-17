using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorializer : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            gameObject.SetActive(false);
    }
}
