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
        public EmpolyeeBL(int id, string name, bool manager)
        {
            setId(id);
            Name = name;
            Manager = manager;
        }
        private int Id;
        public void setId(int idE)
        {
            if (idE < 99999999 || idE > 999999999)
            {
                throw new UnValidIdException(idE, "Employee");
            }
            Id = idE;
        }
        public int getId()
        {
            return Id;
        }
        public string Name { get; set; }
        public bool Manager { get; set; }
    }
}
