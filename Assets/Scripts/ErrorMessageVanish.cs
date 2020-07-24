using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessageVanish : MonoBehaviour
{
    public GameObject This;

    bool counting = false;
    float timer;

    public void CreateErrorMessage() {
        counting = true;
        timer = 0f;
    }

    void Update() {
        if(counting) {
            timer += Time.deltaTime;
        }

        if(timer > 2f) {
            counting = false;
            This.SetActive(false);
        }
    }
}
