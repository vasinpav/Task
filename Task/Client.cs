using System.Collections.Generic;

namespace Client
{
    internal abstract class Client<T, B>
    {
        protected B view;
        protected bool isNotStoped;
        public Client(B view) 
        {
            this.view = view;
            isNotStoped = true;
        }
        protected void Stop() 
        {
            isNotStoped = false;
            Disonnect();
        }
        abstract protected void Connect();

        abstract protected void Disonnect();

        abstract protected void ShowData(List<T> data);
    }
}
