using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FruitInfo
{
    public string fruitName;
    public int score ;
}
public class ShowFruit : MonoBehaviour
{
    private bool ShowTheFruit = true;
    public bool LimitCheck = true;
    void Start()
    {
        if (transform.position.y < 900 )
        {
            ShowTheFruit = false;
        }
    }

    void Update()
    {
        if (ShowTheFruit == true)
        {
            GetComponent<Transform>().position = FruitSpawn.SpawnPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().gravityScale = 60;
            ShowTheFruit = false;
            FruitSpawn.spawnFinish = false;
            StartCoroutine(CheckGameOver());
        }
        
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.name == "LineLimit") && (LimitCheck == true))
        {
            Debug.Log("GameOver");
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == gameObject.tag)
        {
            FruitSpawn.FruitPos = transform.position;
            FruitSpawn.newFruit = true;
            FruitSpawn.FruitCombine = int.Parse(gameObject.tag);
            Destroy(gameObject);
        }
    }

    IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(.5f);
        LimitCheck = false;
    }
}
