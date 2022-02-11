using System.Collections.Generic;

namespace Client
{
    internal interface IProtocol<T>
    {
        public List<T> buildFromBytes(byte[] byteArray);
        public byte[] getBytesFrom(List<T> data);
    }
}
