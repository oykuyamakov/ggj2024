using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Roro.Scripts.UI.UITemplates.UITemplateImplementations
{
    [RequireComponent(typeof(Slider))]
    public class SliderTemplate : UITemplate<float>
    {
        private Slider m_Slider => GetComponent<Slider>();
        
        
        private Tween m_Tween;

        public override void Set(float val)
        {
            m_Slider.value = val;
        }
        public void Set(float value, float maxVal)
        {
            var newVal = math.remap(0, maxVal, 0, 1, value);
            m_Slider.value = newVal;
        }

        public Slider GetSlider()
        {
            return m_Slider;
        }
        
        public void AnimatedSet(float value, float dur, float maxVal)
        {
            var newVal = math.remap(0, maxVal, 0, 1, value);
            m_Tween?.Kill();
            m_Tween = DOTween.To(val => m_Slider.value = val, m_Slider.value, newVal, dur);
        }

        public override void Enable()
        {
            m_Slider.enabled = true;
        }

        public override void Disable()
        {
            m_Slider.enabled = false;
        }

        public float GetCurrentValue()
        {
            return m_Slider.value;
        }
        public override UIElementType GetElementType()
        {
            throw new System.NotImplementedException();
        }
    }
}