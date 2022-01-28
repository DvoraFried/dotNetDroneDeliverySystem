using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    //this is a big no no - the class config shouldnt be here, it is just a temperery solution to check if the xml works
    public class Config
    {
        public static double available = 0.0003;
        public static double carryLightWeight = 0.0005;
        public static double carrymediumWeight = 0.001;
        public static double carryHeavyWeight = 0.0015;
        public static double DroneLoadingRate = 43.3;
    }
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"..\..\..\..\xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

    
}
