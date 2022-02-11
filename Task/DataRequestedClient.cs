using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    internal class DataRequestedClient : SocketClient<Car, ListBox>, IClient
    {
        IDataSavable<Car> dataSaver;
        List<Car> curentCars;
        public DataRequestedClient(string server, int port, ListBox listBox) : base(server, port, listBox)
        {
            dataSaver = new DataSaverXml();
        }

        public void RequestAll()
        {
            int code = -1;
            SendRequest(code);
        }

        public void RequestFromId(int id)
        {
            SendRequest(id);
        }

        private void SendRequest(int id) 
        {
            byte[] requestData = BitConverter.GetBytes(id);
            try
            {
                socketClient.Send(requestData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Stop();
            }
        }

        public void SaveData()
        {
            dataSaver.SaveData(curentCars);
        }

        
        protected override void Receive(List<Car> cars)
        {
            curentCars = cars;
            ShowData(cars);
        }

        protected override void ShowData(List<Car> data)
        {
            List<string> dataForShow = new List<string>();

            foreach(var car in data) 
            {
                string carInf = $"марка: {car.Brand};  год выпуска:  {car.YearOfIssue};  объем двигателя: {car.EngineCapacity};  число дверей: {car.NumDoors}";
                dataForShow.Add(carInf);
            }

            Application.Current.Dispatcher.Invoke(() => view.ItemsSource = dataForShow);
 
        }

        public new void Stop()
        {
            base.Stop();
        }
    }
}
