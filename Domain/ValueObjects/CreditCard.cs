using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class CreditCard
    {

        [Column("Type")] public CreditCardType Type { get;  set; }

        [Column("ExpirationDate")] public DateTime ExpirationDate { get;  set; }

        [Column("OwnerName")] public string OwnerName { get;  set; }

        [Column("CardNumber")] public string CardNumber { get;  set; }

        [Column("SecurityNumber")] public string SecurityNumber { get;  set; }


        public static CreditCard NullCreditCard = new CreditCard();

        public CreditCard(CreditCardType cardType, string cardNumber, string securityNumber, string ownerName, DateTime expiration)
        {
            string val = this.ValidateTarjeta(cardType, cardNumber);
            if(val != "")
            {
                throw new Exception(val);
            }

            if (expiration < DateTime.Now)
            {
                throw new Exception("Tarjeta vencidad");
            }

            Type = cardType;
            CardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : "";
            SecurityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : "";
            OwnerName = !string.IsNullOrWhiteSpace(ownerName) ? ownerName : "";
            ExpirationDate = expiration;
            
        }


        private string ValidateTarjeta(CreditCardType cardType, string cardNumber)
        {
            if (cardType == CreditCardType.Visa || cardType == CreditCardType.Mastercard)
            {
                if (cardNumber.Trim().Length != 16)
                {
                    return "Numero Tarjeta invalido";
                }
            }
            if(cardType == CreditCardType.Amex)
            {
                if (cardNumber.Trim().Length != 15)
                {
                    return "Numero Tarjeta invalido";
                }
            }
            return "";
        }
        

        protected CreditCard()
        {
            Type = CreditCardType.Unknown;
            ExpirationDate = DateTime.UtcNow;
            CardNumber = string.Empty;
            OwnerName = string.Empty;
            SecurityNumber = string.Empty;
        }
    }
}
