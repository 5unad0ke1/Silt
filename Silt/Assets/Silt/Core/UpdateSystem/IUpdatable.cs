namespace Silt.Core
{
    public interface IUpdatable
    {
        int Priority { get; }
        void OnUpdate();
    }
}