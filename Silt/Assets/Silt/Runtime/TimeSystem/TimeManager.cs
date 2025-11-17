using System;
using UnityEngine;

namespace Silt.Runtime.TimeSystem
{
    public static class TimeManager
    {
        public static bool IsRent => _isRent;
        public static TimeScaleHandler Rent()
        {
            if (_isRent)
                throw new InvalidOperationException("A TimeSystem is already rented. Please return it before renting a new one.");
            _isRent = true;

            _rentCount++;
            return new TimeScaleHandler(_rentCount);
        }
        public static void Free(TimeScaleHandler handler)
        {
            if (!_isRent)
                throw new InvalidOperationException("No TimeSystem is currently rented.");
            if (handler.Id != _rentCount)
                throw new InvalidOperationException("The TimeSystem being returned does not belong to TimeManager.");

            _isRent = false;
            SetScale(_rentCount, 1f);
        }
        internal static void SetScale(int id, float scale)
        {
            if (id != _rentCount)
                throw new InvalidOperationException($"The TimeSystem being modified does not belong to TimeManager.");
            Time.timeScale = scale;
        }
        internal static float GetScale()
        {
            return Time.timeScale;
        }

        private static bool _isRent = false;
        private static int _rentCount = 0;
    }
}