using System;
using System.Collections.Generic;
using System.Text;
using CRM.Buzlogic.Ticket.TicketOrder;

namespace BLL.Ticket
{
    public class TicketImport
    {
        public static TicketOrder GetTicketOrder(int orderId)
        {
            TicketOrder ticket = new TicketOrder();
            bool isSuccess = ticket.LoadTicketOrder(orderId.ToString());
            return ticket;
        }
    }
}
