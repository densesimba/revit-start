using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tra
{
    class Program
    {
        static void Main(string[] args)
        {
           Transaction p1 = new Transaction("001", "9/33/2222", 902839.23);

            p1.showTransaction();
            Console.ReadKey();
        }
    }

            public interface ITransactions
        {
            // interface members
            void showTransaction();
            double getAmount();
        }
        public class Transaction : ITransactions
        {
            private string tCode;
            private string date;
            private double amount;

            public Transaction()
            {
                tCode = " ";
                date = " ";
                amount = 0.0;
            }
            public Transaction(string c, string d, double a)
            {
                tCode = c;
                date = d;
                amount = a;
            }
            public double getAmount()
            {
                return amount;
            }
            public void showTransaction()
            {
                Console.WriteLine("Transaction: {0}", tCode);
                Console.WriteLine("Date: {0}", date);
                Console.WriteLine("Amount: {0}", getAmount());
            }
        }
        
}
   

   