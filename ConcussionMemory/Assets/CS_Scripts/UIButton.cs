using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour {

    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;

    public Color highlightColor = Color.cyan;

    public void OnMouseOver() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) {
            sprite.color = highlightColor;
        }
    }

    public void OnMouseExit() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) {
            sprite.color = Color.white;
        }
    }

    public void OnMouseUp() {
        transform.localScale = Vector3.one;
        if (targetObject != null) {
            targetObject.SendMessage(targetMessage);
        }
    }

    public void OnMouseDown() {
        transform.localScale = new Vector3(.99f, .99f, .99f);
    }
}
