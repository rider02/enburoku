using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    SpriteRenderer mainSpriteRenderer;

    public void Init()
    {
        //このobjectのSpriteRendererを取得
        mainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void setSprite(string spriteName)
    {
        mainSpriteRenderer.sprite = Resources.Load<Sprite>("Image/BackGrounds/" + spriteName);
    }

}
