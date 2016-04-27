using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Persons
{
    [Serializable]
    public class Person : IDeserializationCallback {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime StoreDate;

        [NonSerialized] private string _serialNumber;

        public Person() {
            
        }

        public Person(string name, string address, string phone) {
            Name = name;
            Address = address;
            Phone = phone;
       }

        public void Serialize(string fileName) {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            BinaryFormatter binFormatter = new BinaryFormatter();
            StoreDate = DateTime.Now;
            binFormatter.Serialize(fileStream, this);
            fileStream.Close();
        }

        private string MakeSerialNumber(int counter) {
            return counter < 10 ? "0" + counter.ToString() : counter.ToString();
        }

        public string CreateFileName(int counter) {
            _serialNumber = MakeSerialNumber(counter);
            return string.Join("", @"Person", _serialNumber, @".dat");
        }

        public static Person Deserialize(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            BinaryFormatter binFormatter = new BinaryFormatter();
            var person = (Person)binFormatter.Deserialize(fileStream);
            fileStream.Close();

            return person;
        }

        public void OnDeserialization(object sender) {
            _serialNumber = MakeSerialNumber(PersonForm.Counter);
        }

        public override string ToString() {
            return Name +" " +  Address;
        }

        public override bool Equals(object obj) {
            if (obj.GetType().IsAssignableFrom(GetType())) {
                Person otherPerson = (Person) obj;
                return Name.Equals(otherPerson.Name) && Address.Equals(otherPerson.Address) && 
                    Phone.Equals(otherPerson.Phone);
            }
            return false;
        }
    }
}
