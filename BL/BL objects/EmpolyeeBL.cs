using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;

namespace BO
{
    public class EmpolyeeBL
    {
        #region CTOR
        public EmpolyeeBL(int id, string name, bool manager)
        {
            Id = id;
            Name = name;
            Manager = manager;
        }
        #endregion

        private int id;
        #region SET-GET_ID
        public int Id
        {
            get { return id; }
            set {
                if (value < 99999999 || value > 999999999)
                {
                    throw new UnValidIdException(value, "Employee");
                }
                id = value;
            }
        }
        #endregion

        public string Name { get; set; }
        public bool Manager { get; set; }
    }
}
