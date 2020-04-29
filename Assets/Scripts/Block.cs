using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config Params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;


    // Cached refs
    Level level;
    GameSession gameStatus;

    // State variables
    [SerializeField] int timesHit;  // Serialized only for debugging. 



    private void Start()
    {
        level = FindObjectOfType<Level>();
        CountBreakableBlocks();

        gameStatus = FindObjectOfType<GameSession>();

    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
        //Debug.Log(collision.gameObject.name);

    }

    private void HandleHit()
    {
        int maxHits = hitSprites.Length + 1;
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else {
            ShowNextSprite();
        }
    }

    private void ShowNextSprite()
    {
       int spriteIndex = timesHit -1;
       if (hitSprites[spriteIndex] != null)
       {
          GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
       }
       else{
           Debug.LogError("Block sprite missing from array.");
        }

    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        level.BlockDestroyed();
        gameStatus.AddToScore();
        TriggerSparklesVFX();

    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
    }

    private void TriggerSparklesVFX(){
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
