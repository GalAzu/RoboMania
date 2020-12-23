using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorescript : MonoBehaviour
{
    Text scoretext;
    Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
        scoretext = GetComponent<Text>();
    }

    void Update()
    {
        // player.score.ToString(); turns the score into string
        scoretext.text = player.score.ToString();
    }
}
