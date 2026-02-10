namespace Silt.Services
{
    public interface IInjectable<T0>
    {
        void Inject(T0 arg0);
    }
    public interface IInjectable<T0, T1>
    {
        void Inject(T0 arg0, T1 arg1);
    }
    public interface IInjectable<T0, T1, T2>
    {
        void Inject(T0 arg0, T1 arg1, T2 arg2);
    }
    public interface IInjectable<T0, T1, T2, T3>
    {
        void Inject(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    }
}