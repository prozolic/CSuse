using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using static MonochromeEffect;

[ExecuteInEditMode]
public class NegativePositiveInversionEffect : MonoBehaviour
{
    [SerializeField]
    private EffectSettings _effectSettings = new EffectSettings(1f);

    private EffectSettings _oldEffectSettings;
    private Material _material;

    public bool EnableEffect => _effectSettings.enable && this.Material != null;
    public Material Material => _material;

    [Serializable]
    public struct EffectSettings
    {
        public bool enable;
        [SerializeField, Range(0f, 1f)] public float rate;

        public EffectSettings(float defaultRate)
        {
            enable = false;
            rate = defaultRate;
        }
    }

    private void Awake()
    {
        _oldEffectSettings = _effectSettings;

        _material = CoreUtils.CreateEngineMaterial(Shader.Find("NegativePositiveInversion"));
    }

    private void OnValidate()
    {
        if (_effectSettings.enable)
            this.UpdateMaterial();
        else
            this.DisposeMaterial();

        _oldEffectSettings = _effectSettings;
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
        if (_material == null |
            math.abs(_effectSettings.rate - _effectSettings.rate) <= float.Epsilon)
        {
            this.DisposeMaterial();
            _material = CoreUtils.CreateEngineMaterial(Shader.Find("NegativePositiveInversion"));
        }
        _material.SetFloat(Shader.PropertyToID("_InversionRate"), _effectSettings.rate);
    }
}
