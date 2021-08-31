using System;
using Rest.Api.Models;

namespace Rest.Api.Features
{
    public static class TransactionExtensions
    {
        public static TransactionDto ToDto(this Transaction transaction)
        {
            return new ()
            {
                TransactionId = transaction.TransactionId
            };
        }
        
    }
}
