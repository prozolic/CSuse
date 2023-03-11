using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class MonochromeEffect : MonoBehaviour
{
    [SerializeField] 
    private EffectSettings _effectSettings = new EffectSettings(EffectPattern.Binary);
    [SerializeField]
    private SepiaSettings _sepiaSettings = new SepiaSettings(1f);

    private SepiaSettings _oldSepiaSettings;
    private Material _material;

    public Material Material => _material;
    public bool EnableEffect => _effectSettings.enable && this.Material != null;

    [Serializable]
    public struct EffectSettings
    {
        public bool enable;
        public EffectPattern effectPattern;

        public EffectSettings(EffectPattern defaultPattern)
        {
            enable = false;
            effectPattern = defaultPattern;
        }
    }

    [Serializable]
    public struct SepiaSettings
    {
        [SerializeField, Range(0f, 1f)] public float rate;

        public SepiaSettings(float defaultRate)
        {
            rate = defaultRate;
        }
    }

    public enum EffectPattern
    {
        Monochrome,
        Binary,
        Sepia
    }

    private void Awake()
    {
        _oldSepiaSettings = _sepiaSettings;

        this.UpdateMaterial();
    }

    private void OnValidate()
    {
        // Œø‰Ê‚ð“K—p‚µ‚È‚¢ê‡
        if (_effectSettings.enable)
            this.UpdateMaterial();
        else
            this.DisposeMaterial();

        _oldSepiaSettings = _sepiaSettings;
    }

    private void OnDestroy() => this.DisposeMaterial();
    private void DisposeMaterial()
    {
        if (_material != null)
        {
            CoreUtils.Destroy(_material);
            _material = null;
        }
    }

    private void UpdateMaterial()
    {
        switch (_effectSettings.effectPattern)
        {
            case EffectPattern.Monochrome:
                _material = CoreUtils.CreateEngineMaterial(Shader.Find("Monochrome"));
                break;
            case EffectPattern.Binary:
                _material = CoreUtils.CreateEngineMaterial(Shader.Find("Binary"));
                break;
            case EffectPattern.Sepia:
                this.UpdateSepiaMaterial();
                break;
        }
    }

    private void UpdateSepiaMaterial()
    {
        if (_material == null | 
            math.abs(_sepiaSettings.rate - _oldSepiaSettings.rate) <= float.Epsilon)
        {
            this.DisposeMaterial();
            _material = CoreUtils.CreateEngineMaterial(Shader.Find("Sepia"));
        }
        _material.SetFloat(Shader.PropertyToID("_SepiaRate"), _sepiaSettings.rate); 
    }
}
