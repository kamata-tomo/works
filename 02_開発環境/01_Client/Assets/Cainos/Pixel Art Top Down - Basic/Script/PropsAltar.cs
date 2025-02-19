﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//when something get into the alta, make the runes glow


public class PropsAltar : MonoBehaviour
{
    public List<SpriteRenderer> runes;
    public float lerpSpeed;

    private Color curColor;
    private Color targetColor;

    [SerializeField] GameManager gameManager;


    private void Awake()
    {
        targetColor = runes[0].color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        targetColor.a = 1.0f;
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("ggggggggg");
            gameManager.GameClear();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        targetColor.a = 0.0f;
    }


    private void Update()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

        foreach (var r in runes)
        {
            r.color = curColor;
        }
    }
}

