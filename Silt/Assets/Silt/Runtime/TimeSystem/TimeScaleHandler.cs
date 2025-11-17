namespace Silt.Runtime.TimeSystem
{
    public readonly struct TimeScaleHandler
    {
        internal TimeScaleHandler(int id)
        {
            _id = id;
        }

        public float Scale
        {
            get => TimeManager.GetScale();
            set => TimeManager.SetScale(_id, value);
        }
        internal int Id => _id;

        private readonly int _id;
    }
}