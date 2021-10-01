using System;

namespace FrontierAccountApp
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double? AmountDue { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public int AccountStatusId { get; set; }
    }
}