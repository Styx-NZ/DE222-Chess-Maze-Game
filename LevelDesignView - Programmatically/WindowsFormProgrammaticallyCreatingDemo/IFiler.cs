namespace FilerNS
{
    public interface IFiler
    {
        void Save(string filename, IFileable callMeBackforDetails);
        string Load(string filename);
    }
}
