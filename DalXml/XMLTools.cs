﻿using System.Runtime.CompilerServices;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static DalFacade.DalApi.Exeptions.Exceptions;


namespace Dal
{
    class DL
    {

        public class XMLTools
        {
            #region SaveLoadWithXMLSerializer
            /// <summary>
            /// generic function to add list to an xml file by its path
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="list"></param>
            /// <param name="filePath"></param>
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
                    //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
                }
            }
            /// <summary>
            /// generic function to get object lists from xml file using the path
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="filePath"></param>
            /// <returns></returns>
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
                    throw new XMLFileLoadCreateException(filePath);
                }
                throw new Exception();
            }
            #endregion

            #region SaveLoadWithLinqToXml
            /// <summary>
            /// function add drones list to the xml file; using linq to xml
            /// </summary>
            /// <param name="list"></param>
            /// <param name="filePath"></param>
            public static void SaveDronesWithXElement(IEnumerable<Drone> list, string filePath)
            {
                XElement root = new XElement("drones", 
                    from e in list select createDroneElement(e));
                root.Save(filePath);
            }
            public static XElement createDroneElement(Drone e)
            {
                return new XElement("drone",
                        new XElement("Id", e.Id),
                        new XElement("isActive", e.IsActive),
                        new XElement("Battery", e.Battery), 
                        new XElement("MaxWeight", e.MaxWeight), 
                        new XElement("Model", e.Model)
                    );
            }
            /// <summary>
            /// funcction returns drones list from a xml flie using the path and 
            /// </summary>
            /// <param name="filePath"></param>
            /// <returns></returns>
            public static IEnumerable<Drone> LoadListWithXElement(string filePath)
            {
               if (File.Exists(filePath))
                   return (from e in XElement.Load(filePath).Elements() 
                           select new Drone() {
                               Battery = Convert.ToDouble(e.Element("Battery").Value),
                               Id = Convert.ToInt32(e.Element("Id").Value),
                               IsActive = Convert.ToBoolean(e.Element("isActive").Value), 
                               MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), e.Element("MaxWeight").Value),
                               Model = e.Element("Model").Value
                           });
               throw new Exception(); // - not exist
            }
            #endregion
        }

    }

}
