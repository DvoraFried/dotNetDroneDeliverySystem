using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    class DL
    {

        public class XMLTools
        {
            #region SaveLoadWithXMLSerializer
            public static void SaveListToXMLSerializer<T>(IEnumerable<T> list, string filePath)
            {
                try
                {
                    FileStream file = new FileStream(filePath, FileMode.Create);
                    XmlSerializer x = new XmlSerializer(list.GetType());
                    x.Serialize(file, list);
                    file.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
                }
            }
            public static IEnumerable<T> LoadListFromXMLSerializer<T>(string filePath)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        IEnumerable<T> list;
                        XmlSerializer x = new XmlSerializer(typeof(List<T>));
                        FileStream file = new FileStream(filePath, FileMode.Open);
                        list = (IEnumerable<T>)x.Deserialize(file);
                        file.Close();
                        return list;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);  // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
                }
                throw new Exception();
            }
            #endregion
            #region SaveLoadWithLinqToXml
            public static void SaveParcelsWithXElement<T>(IEnumerable<Parcel> list, string filePath)
            {
                XElement root = new XElement("parcels", 
                    from e in list select createParcelElement(e));
                root.Save(filePath);
            }
            public static void SaveParcelsWithXElement<T>(XElement root, string filePath)
            {
                root.Save(filePath);
            }
            public static XElement createParcelElement(Parcel e)
            {
                return new XElement("parcel",
                        new XElement("Id", e.Id),
                        new XElement("SenderId", e.SenderId),
                        new XElement("TargetId", e.TargetId),
                        new XElement("isActive", e.isActive),
                        new XElement("Requested", e.Requested),
                        new XElement("Scheduled", e.Scheduled),
                        new XElement("PickUp", e.PickUp),
                        new XElement("Delivered", e.Delivered),
                        new XElement("Priority", e.Priority),
                        new XElement("Weight", e.Weight),
                        new XElement("DroneId", e.DroneId)
                    );
            }
            public static XElement LoadListWithXElement<T>(string filePath)
            {
               if (File.Exists(filePath))
                   return XElement.Load(filePath);
               throw new Exception(); // - not exist
            }
            #endregion
        }

    }

}
