using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefconZ.UI
{
    public class SliderBar : MonoBehaviour
    {
        public Image sliderBar;
        public Text sliderLabel;
        public Gradient gradient;

        // Transform sizes
        public float rectMaxX;
        public float rectMinX;
        // Display values
        public float valueMax;
        public float valueMin;

        /// <summary>
        /// Sets the value values if not already set
        /// </summary>
        public void InitSliderBar(float valueMax, float valueMin)
        {
            this.rectMinX = 0.0f;
            this.rectMaxX = sliderBar.rectTransform.sizeDelta.x;
            this.valueMax = valueMax;
            this.valueMin = valueMin;
        }

        /// <summary>
        /// Updates The Slider Bar
        /// </summary>
        /// <param name="currentValue"></param>
        public void UpdateSlider(float currentValue, float valueMax)
        {
            // update the stored values
            this.valueMax = valueMax;

            // Update the label text
            sliderLabel.text = currentValue.ToString("n0") + "/" + valueMax.ToString("n0");

            float percentage = currentValue / valueMax; // Calculate the current fill percentage
            sliderBar.rectTransform.sizeDelta = new Vector2(rectMaxX * percentage, sliderBar.rectTransform.sizeDelta.y);

            // Set the slider color to the current gradient percentage
            sliderBar.color = gradient.Evaluate(percentage);
        }
    }
}