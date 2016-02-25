using UnityEngine;
using System.Collections;

public class Backdrop : MonoBehaviour {
	
    SpriteRenderer sprite;

    public bool IsSolved {get;set;}

	void Start () {
        sprite = GetComponentInChildren<SpriteRenderer>();
	}
		
	void Update () {
        if (IsSolved && sprite.color != Color.white)            
        {
            sprite.color = Color.Lerp(sprite.color, Color.white, .1f);
        }
	}
}
