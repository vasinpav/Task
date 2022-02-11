using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    internal class CarProtocol : IProtocol<Car>
    {
        public List<Car> buildFromBytes(byte[] byteArray)
        {
            List<Car> cars = new List<Car>();
            int countByte = 0;
            while (countByte != byteArray.Count()) 
            {
                byte numFields = byteArray[countByte + 1];
                byte countFields = 0;

                string? brand = null;
                ushort? yearOfIssue = null;
                float? engineCapacity = null;
                ushort? numDoors = null;

                countByte += 2;

                while (countFields != numFields) 
                {
                    switch (byteArray[countByte]) 
                    {
                        case 0x09: 
                            {
                                int lenth = byteArray[countByte + 1];
                                byte[] brandBytes = new byte [lenth];
                                for (int i = countByte + 2, j = 0; j < lenth; i++, j++) 
                                {
                                    brandBytes[j] = byteArray[i];
                                }
                                
                                brand = Encoding.ASCII.GetString(brandBytes);
                                countByte += lenth + 2;
                                countFields++;
                                break;
                            }

                        case 0x12:
                            {
                                byte[] num = new byte[] { byteArray[countByte + 1], byteArray[countByte + 2] };
                                ushort numVal = BitConverter.ToUInt16(num, 0);
                                
                                if (yearOfIssue == null) 
                                {
                                    yearOfIssue = numVal;
                                }

                                else
                                {
                                    numDoors = numVal;
                                }

                                countFields++;
                                countByte += 3;
                                break;
                            }

                        case 0x13:
                            {
                                byte[] num = new byte[] { byteArray[countByte + 1], byteArray[countByte + 2], byteArray[countByte + 3], byteArray[countByte + 4] };
                                engineCapacity = BitConverter.ToSingle(num, 0);
                                countByte += 5;
                                countFields++;
                                break;
                            }
                    }
                }
                Car car = new Car(brand, yearOfIssue, engineCapacity, numDoors);
                cars.Add(car);
            }
            return cars;
        }

        public byte[] getBytesFrom(List<Car> data)
        {
            List<byte> bytes = new List<byte>();
            foreach(var car in data) 
            {
                int startSize = bytes.Count;
                bytes.Add(0x02);
                
                byte countItem = 0;
                
                if(car.Brand != null)
                {
                    countItem++;
                    bytes.Add(0x09);
                    bytes.Add((byte)car.Brand.Length);
                    bytes.AddRange(Encoding.ASCII.GetBytes(car.Brand));
                }
                if(car.EngineCapacity != null) 
                {
                    countItem++;
                    bytes.Add(0x13);
                    BitConverter.GetBytes(5.5f);
                    bytes.AddRange(BitConverter.GetBytes((float)car.EngineCapacity));
                }
                if (car.YearOfIssue != null)
                {
                    countItem++;
                    bytes.Add(0x12);
                    bytes.AddRange(BitConverter.GetBytes((ushort)car.YearOfIssue));
                }
                if (car.NumDoors != null)
                {
                    countItem++;
                    bytes.Add(0x12);
                    bytes.AddRange(BitConverter.GetBytes((ushort)car.NumDoors));
                }

                bytes.Insert(startSize+1, countItem);
                
            }

            return bytes.ToArray();
        }
    }
}
