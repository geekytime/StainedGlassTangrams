using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Fader : MonoBehaviour {

    public bool IsVisible;
    SpriteRenderer sprite;

	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	
	void Update () {
        if (IsVisible && sprite.color != Color.white)
        {
            sprite.color = Color.Lerp(sprite.color, Color.white, .1f);
        } else if (!IsVisible && sprite.color != Color.clear)
        {
            sprite.color = Color.Lerp(sprite.color, Color.clear, .1f);
        }

	}
}
