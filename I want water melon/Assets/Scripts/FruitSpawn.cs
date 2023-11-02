using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using DG.Tweening;

public class FruitSpawn : MonoBehaviour
{
    public Transform[] fruitPrefabs;
    private bool canSpawn = true;
    public static bool spawnFinish = false;
    static public Vector2 SpawnPos;
    static public Vector2 FruitPos;
    public static  bool newFruit = false;
    public static int FruitCombine = 0;
    private Rigidbody2D rb;
    public FruitInfo[] fruitInfos;
    public int score = 0;
    public TMP_Text Score;
    public TMP_Text comboScoreText;
    public TMP_Text MelonCount;

    private int melonCount = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canSpawn && Input.GetMouseButtonDown(0))
        {
            canSpawn = false;
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0;
            SpawnRandomFruit();
            Invoke("EnableSpawn", 2.0f);
        }
        // will make 1 edition for pc if have time 
        // if (Input.GetKey("a"))
        // {
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(-150, 0);
        // }
        // if (Input.GetKey("d"))
        // {
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(150, 0);
        // }
        // if ((!Input.GetKey("a") && (!Input.GetKey("d"))))
        // {
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        // }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    rb.velocity = new Vector2(-150, 0);
                }
                else
                {
                    rb.velocity = new Vector2(150, 0);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }
        }
        CreateNewFruit();
        SpawnPos = transform.position;
    }

    void SpawnRandomFruit()
    {
        if (spawnFinish == false)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(fruitPrefabs[Random.Range(0,5)], spawnPosition, Quaternion.identity, transform.parent);
            spawnFinish = true;
        }
    }
    
    void EnableSpawn()
    {
        canSpawn = true;
    }
    void CreateNewFruit()
    {
        if (newFruit == true)
        {
            newFruit = false;

            if (FruitCombine < fruitPrefabs.Length - 1)
            {
                Instantiate(fruitPrefabs[FruitCombine], FruitPos, Quaternion.identity,transform.parent);
                Vibration.Vibrate(30);
                FruitInfo fruitInfo = fruitInfos[FruitCombine];
                score += fruitInfo.score;
                Score.text =  score.ToString();
                comboScoreText.text = "+" + fruitInfo.score;
                comboScoreText.transform.position = FruitPos;
                comboScoreText.transform.DOMoveY(FruitPos.y + 100.0f, 1.0f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    comboScoreText.transform.gameObject.SetActive(false);
                });
                comboScoreText.transform.gameObject.SetActive(true);
                if (gameObject.tag == "9")
                {
                    melonCount++;
                    MelonCount.text = "Fruit 9 Count: " + melonCount;
                }
            }
        }
    }
}
