using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
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

        offset.x = transform.position.x / transform.localScale.x;
        offset.y = transform.position.y / transform.localScale.y;

        mat.SetTextureOffset("_MainTex", offset);
    }
}
