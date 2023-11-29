using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraShader : MonoBehaviour
{

    public Material ToonMaterial;
    // Start is called before the first frame update
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (ToonMaterial == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        Graphics.Blit(src, dest, ToonMaterial);
    }
}