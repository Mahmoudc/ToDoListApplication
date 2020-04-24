using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ToDoList
{
    public class InvItemDB
    {
        private string Path;
        public InvItemDB(string path) {
            Path = @"..\..\"+path+".xml";
        }
        public string getPath()
        {
            return Path;
        }
        public List<ToDoList> GetItems(bool firstFile)
        {
            // create the list
            List<ToDoList> items = new List<ToDoList>();

            // create the XmlReaderSettings object
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            // create the XmlReader object
            
            XmlReader xmlIn = XmlReader.Create(Path, settings);

            // read past all nodes to the first Book node
            if (xmlIn.ReadToDescendant("ToDoList"))
            {
                // create one Product object for each Product node
                do
                {
                    ToDoList list = new ToDoList();
                    xmlIn.ReadStartElement("ToDoList");
                    if(firstFile)
                        list.date = xmlIn.ReadElementContentAsString();
                    list.details= xmlIn.ReadElementContentAsString();
                    items.Add(list);
                }
                while (xmlIn.ReadToNextSibling("ToDoList"));
            }
            
            // close the XmlReader object
            xmlIn.Close();

            return items;
        }

        public void SaveItems(List<ToDoList> items, bool firstFile)
        {
            // create the XmlWriterSettings object
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");

            // create the XmlWriter object
            XmlWriter xmlOut = XmlWriter.Create(Path, settings);

            // write the start of the document
            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("ToDoLists");

            // write each product object to the xml file
            foreach (ToDoList item in items)
            {
                xmlOut.WriteStartElement("ToDoList");
                if(firstFile)
                    xmlOut.WriteElementString("Date", item.date);
                xmlOut.WriteElementString("Description", item.details);
                xmlOut.WriteEndElement();
            }

            // write the end tag for the root element
            xmlOut.WriteEndElement();

            // close the xmlWriter object
            xmlOut.Close();
        }
    }
}
