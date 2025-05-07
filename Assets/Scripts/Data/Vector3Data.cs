using System;

namespace Data
{
    [Serializable]
    public class Vector3Data
    {
        private float _x;
        private float _y;
        private float _z;

        public Vector3Data(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public float X => _x;
        public float Y => _y;
        public float Z => _z;
    }
}