using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    class ParcelByTransfer
    {
        int Id { get; set; }
        PrioritiesBL Priorty { get; set; }
        CustomerOnDelivery SenderCustomer { get; set; }
        CustomerOnDelivery GetterCustomer { get; set; }
    }
}
