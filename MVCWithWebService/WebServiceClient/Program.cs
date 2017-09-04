using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClient
{
    public class Customer
    {
        public int ID { get; set; }

       
        public string LastName { get; set; }
        
        public string FirstMidName { get; set; }

      
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
       
        public DateTime EnrollmentDate { get; set; }

        //   public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
    class Program
    {

         static void Main(string[] args)
        {
            GetAllCustData(); 
            Console.ReadLine();
        }
        public static void GetAllCustData() //Get All Events Records  
        {
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://mvcwithwebservice.azurewebsites.net/api/CustomersWebApi"); //URI  
				Console.WriteLine(Environment.NewLine + result);
            }
        }

    }
}
