using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using static MonochromeEffect;

[ExecuteInEditMode]
public class MosaicEffect : MonoBehaviour
{
    [SerializeField] private EffectSettings _effectSettings = new EffectSettings(4);

    public bool EnableEffect => _effectSettings.enable;

    public int Sampling => Math.Max((int)_effectSettings.sampling,1);

    [Serializable]
    public struct EffectSettings
    {
        public bool enable;
        [SerializeField, Range(1,256)] public uint sampling;

        public EffectSettings(uint initizlieSampling)
        {
            enable = false;
            sampling = initizlieSampling;
        }
    }

}
