namespace Client
{
    internal interface IClient
    {
        void RequestAll();
        void RequestFromId(int id);
        void Stop();
        void SaveData();
    }
}
