using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Clock : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;

    void FixedUpdate()
    {
        DateTime dateTime = DateTime.Now;
        string time = dateTime.ToString("HH:mm");
        _text.SetText(time);
    }
}
