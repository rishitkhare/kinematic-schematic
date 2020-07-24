using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public GameObject[] elements;

    public void ChangeBackground(int val) {
        foreach(GameObject BG in elements) {
            BG.SetActive(false);
        }

        elements[val].SetActive(true);
    }
}
