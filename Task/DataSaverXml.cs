using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Client
{
    internal class DataSaverXml : IDataSavable<Car>
    {
        public void SaveData(List<Car> data)
        {
            XElement cars = new XElement("cars");
            XDocument xdoc = new XDocument();
            foreach (var carItem in data) 
            {
                
                XElement car = new XElement("car");
                
                XElement brandElem = new XElement("brand", carItem.Brand);
                XElement yearOfIssueElem = new XElement("yearOfIssue", carItem.YearOfIssue);
                XElement engineCapacityElem = new XElement("engineCapacity", carItem.EngineCapacity);
                XElement numDoorsElem = new XElement("numDoors", carItem.NumDoors);
               
                car.Add(brandElem);
                car.Add(yearOfIssueElem);
                car.Add(engineCapacityElem);
                car.Add(numDoorsElem);
                cars.Add(car);
            }

            xdoc.Add(cars);
            
            xdoc.Save("cars_"+ DateTime.Now.ToString("yyyy-MM-dd_h-mm-ss") + ".xml");
        }
    }
}
