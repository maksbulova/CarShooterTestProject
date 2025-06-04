using System;
using UnityEngine;

namespace BitToolSet.Extensions
{
    public static class MathExtensions
    {
        public static float Remap(this float value, float oldMin, float oldMax, float newMin, float newMax)
        {
            float t = Mathf.InverseLerp(oldMin, oldMax, value);
            return Mathf.Lerp(newMin, newMax, t);
        }
    
        public static float Clamp(float value, float min, float max, bool clampMin, bool clampMax)
        {
            float returnValue = value;
            if (clampMin && (returnValue < min))
            {
                returnValue = min;
            }
            if (clampMax && (returnValue > max))
            {
                returnValue = max;
            }
            return returnValue;
        }
        
        public static float AngleDifferenceXZ(Vector3 oldDirection, Vector3 newDirection)
        {
            float x1 = oldDirection.x;
            float y1 = oldDirection.z;
            float x2 = newDirection.x;
            float y2 = newDirection.z;
            return FastAtan2(x1 * y2 - y1 * x2, x1 * x2 + y1 * y2) * Mathf.Rad2Deg;
        }
    
        public static float FastAtan2(float y, float x)
        {
            const float ONEQTR_PI = Mathf.PI / 4.0f;
            const float THRQTR_PI = 3.0f * Mathf.PI / 4.0f;
            float r, angle;
            float abs_y = System.Math.Abs(y) + 1e-10f;      // kludge to prevent 0/0 condition
            if (x < 0.0f)
            {
                r = (x + abs_y) / (abs_y - x);
                angle = THRQTR_PI;
            }
            else
            {
                r = (x - abs_y) / (x + abs_y);
                angle = ONEQTR_PI;
            }
            angle += (0.1963f * r * r - 0.9817f) * r;
            if (y < 0.0f)
                return (-angle);     // negate if in quad III or IV
            else
                return (angle);
        }
        
        
        public static string ToRoman(int number)
        {
            // Handle numbers between 0 and 3999
            if (number < 1) return string.Empty;            
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); 
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);            
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            return "I" + ToRoman(number - 1);
        }

    }
}
