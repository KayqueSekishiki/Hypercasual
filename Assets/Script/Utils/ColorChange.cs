using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    private float _duration = 0.2f;
    public Color startColor = Color.white;

    private Color _correctColor;
    [SerializeField] private MeshRenderer meshRenderer;

    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _correctColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);

        meshRenderer.materials[0].DOColor(_correctColor, _duration).SetDelay(0.5f);
    }
}
