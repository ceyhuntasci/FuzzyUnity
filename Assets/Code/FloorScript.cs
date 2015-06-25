﻿using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour
{
    
    public float scrollSpeed = 0.78F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if(GameLogic.State == GameLogic.GameState.OnGoing)
        {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0.04f));
        }
        
    }
}
