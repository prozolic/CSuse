using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class MonochromeEffect : MonoBehaviour
{
    [SerializeField] private EffectSettings _effectSettings = new EffectSettings();

    private Material _material;
    public Material Material => _material;
    public bool EnableEffect => _effectSettings.enable && this.Material != null;

    [Serializable]
    public struct EffectSettings
    {
        public bool enable;
        public EffectPattern effectPattern;
    }

    public enum EffectPattern
    {
        Monochrome,
        Binary
    }

    private void Awake()
    {
        this.UpdateMaterial();
    }

    private void OnValidate()
    {
        this.UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        if (_material != null)
            CoreUtils.Destroy(_material);

        switch (_effectSettings.effectPattern)
        {
            case EffectPattern.Monochrome:
                _material = CoreUtils.CreateEngineMaterial(Shader.Find("Monochrome"));
                break;
            case EffectPattern.Binary:
                _material = CoreUtils.CreateEngineMaterial(Shader.Find("Binary"));
                break;
        }
    }

}
