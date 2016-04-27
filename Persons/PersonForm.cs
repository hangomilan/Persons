using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Persons
{
    public partial class PersonForm : Form
    {
        private List<Person> _persons;
        public static int Counter; 

        public PersonForm()
        {
            InitializeComponent();
            _persons = new List<Person>(100);
            FillPersonsList();
        }

        private void FillPersonsList() {
            for (int i = 0; i < 100; i++) {
                Person newPerson = new Person();
                string fileName = newPerson.CreateFileName(i);
                if (File.Exists(fileName)) {
                    newPerson = Person.Deserialize(fileName);
                    _persons.Add(newPerson);
                    Counter = i;
                }
                
            }
        }

        private void PersonForm_Load(object sender, EventArgs e) {
            if (_persons.Count > 0) {
                FillTextBoxes(0);
            }
        }

        private void FillTextBoxes(int index) {
            ClearTextBoxes();
            nameBox.Text = _persons[index].Name;
            addressBox.Text = _persons[index].Address;
            phoneBox.Text = _persons[index].Phone;
            dateLabel.Text = _persons[index].StoreDate.ToString();
        }

        private void saveButton_Click(object sender, EventArgs e) {
            Person actualPerson = new Person(nameBox.Text, addressBox.Text, phoneBox.Text);
            _persons.Add(actualPerson);
            string fileName = actualPerson.CreateFileName(++Counter);
            actualPerson.Serialize(fileName);
            dateLabel.Text = actualPerson.StoreDate.ToString();
        }

        private void prevButton_Click(object sender, EventArgs e) {
            Person actualPerson = new Person(nameBox.Text, addressBox.Text, phoneBox.Text);
            int position = GetPersonIndex(actualPerson);
            if (position > 0) {
                FillTextBoxes(position - 1);
            }
            else {
                MessageBox.Show(@"No previous item available!", @"Attention");
            }
        }

        private void nextButton_Click(object sender, EventArgs e) {
            Person actualPerson = new Person(nameBox.Text, addressBox.Text, phoneBox.Text);
            int position = GetPersonIndex(actualPerson);
            if (position < _persons.Count - 1) {
                FillTextBoxes(position + 1);
            }
            else {
                MessageBox.Show(@"No next item available!", @"Attention");
            }
        }

        private int GetPersonIndex(Person personToFind) {
            for (int i = 0; i < _persons.Count; i++) {
                if (_persons[i].Equals(personToFind)) {
                    return i;
                }
            }
            return -1;
        }

        private void ClearTextBoxes() {
            nameBox.Text = String.Empty;
            addressBox.Text = String.Empty;
            phoneBox.Text = String.Empty;
            dateLabel.Text = String.Empty;
        }
        
    }
}
