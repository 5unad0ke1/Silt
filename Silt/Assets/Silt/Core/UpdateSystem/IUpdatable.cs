namespace Silt.Update
{
    public interface IUpdatable
    {
        int Priority { get; }
        void OnUpdate();
    }
}