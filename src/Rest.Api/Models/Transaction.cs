using System;

namespace Rest.Api.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; private set; }
        public double Amount { get; private set; }
    }
}
