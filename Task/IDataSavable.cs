using System.Collections.Generic;

namespace Client
{
    internal interface IDataSavable<T>
    {
        public void SaveData(List<T> data);
    }
}
