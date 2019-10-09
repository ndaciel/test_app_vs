using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlCustomer
    {

        public mdlCustomer()
        {
            Latitude = "0";
            Longitude = "0";
        }


        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string CustomerTypeID { get; set; }

        [DataMember]
        public string PIC { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string Radius { get; set; }


        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Channel { get; set; }

        [DataMember]
        public string CountryRegionCode { get; set; }

        [DataMember]
        public string Account { get; set; }

        [DataMember]
        public string Distributor { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string Mobilephone1 { get; set; }

        [DataMember]
        public string Mobilephone2 { get; set; }

        public string BankAccountNumber { get; set; }
        
    }

    public class mdlCustomerForAddNew
    {
        [DataMember]
        public string RowNumber { get; set; }

        [DataMember]
        public string Total { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string CustomerTypeID { get; set; }

        [DataMember]
        public string PIC { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string Radius { get; set; }


        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string CountryRegionCode { get; set; }

        [DataMember]
        public string Account { get; set; }

        [DataMember]
        public string Distributor { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }
    }

    public class mdlCustomer2
    {
        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string CustomerType { get; set; }
    }

    public class mdlCustomerParam
    {
        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string CustomerTypeID { get; set; }

        [DataMember]
        public string PIC { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string BranchID { get; set; }
    }

    public class mdlCustomerList
    {
        public List<mdlCustomer> CustomerList { get; set; }
    }

    //fernandes
    public class mdlCustomerLicense
    {
        [DataMember]
        public string LicenseKey { get; set; }
    }
   
}
