using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
 public int points;
 public int hitsToBreaks;
 public Sprite hitSprite;
 
 
 public void BrickBreak()
 {
     hitsToBreaks--;
     GetComponent<SpriteRenderer>().sprite = hitSprite;
 }
}
