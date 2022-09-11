using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float xSpeed = 1;
    public float ySpeed = 1;

    void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Texture tex = mat.GetTexture("_MainTex");

        tex.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.GetTextureOffset("_MainTex");

        offset.x += xSpeed * Time.deltaTime;
        offset.y += ySpeed * Time.deltaTime;

        mat.SetTextureOffset("_MainTex", offset);
    }
}
