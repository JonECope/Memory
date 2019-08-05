using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card
{
    public string path_to_audio;
    public string path_to_img;
    public string[] choices;
    public int correct_option;
}
