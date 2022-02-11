namespace Server
{
    internal abstract class ComonServer
    {
        protected bool isNotStoped;

        public ComonServer() 
        {
            isNotStoped = true;
        }

        protected abstract void Receve();

        protected abstract void Send(int id);

        protected abstract void Disconnect();

        protected void Stop() 
        {
            isNotStoped = false;
            Disconnect();
        }
    }
}
