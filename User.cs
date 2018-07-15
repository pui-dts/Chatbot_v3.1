using System;

namespace BotApplication

{
    [Serializable]
    public class User
    {

      
        
        //Massage for translate
        public string MassegeWelcom { get; set; }
        public string MassegeAccount { get; set; }
        public string MassegeUsername { get; set; }
        public string MassegePassword { get; set; }
        public string MassegeIncidents { get; set; }
        public string MassegeIncidents2 { get; set; }
        public string MassegeProduct { get; set; }
        public string MassegePriority { get; set; }
        public string MassegeIssueFrom { get; set; }
        public string MassageRating { get; set; }
        public string MassegeThankyou { get; set; }

        //option
        public string Product1 { get; set; }
        public string Product2 { get; set; }
        public string Product3 { get; set; }

        public string Priority1 { get; set; }
        public string Priority2 { get; set; }
        public string Priorityth1 { get; set; }
        public string Priorityth2 { get; set; }


        public string IssueFrom1 { get; set; }
        public string IssueFrom2 { get; set; }
        public string IssueFromth1 { get; set; }
        public string IssueFromth2 { get; set; }


        public string Yes { get; set; }
        public string No { get; set; }
        public string Yes2 { get; set; }
        public string No2 { get; set; }



        //User info 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Incidents { get; set; }
        public string Expire { get; set; }

        public string ImageRate1Url1 { get; set; }
        public string ImageRate1Url2 { get; set; }
        public string ImageRate1Url3 { get; set; }
        public string ImageRate1Url4 { get; set; }
        public string ImageRate1Url5 { get; set; }





        public User()
        {
            //Massage for translate
            this.MassegeWelcom = "Welcome to Metro Systems MA Service, Do you have an account with us?";
            this.MassegeUsername = "Please enter your username";
            this.MassegePassword = "Plese input your password";
            this.MassegeIncidents = "Your current number of incidents available";
            this.MassegeIncidents2 = "Do You want to open ticket?";
            this.MassegeProduct = "Please select the Product";
            this.MassegePriority = "Please select the priority";
            this.MassegeIssueFrom = "Please select issue form";
            this.MassageRating = "Please rate our service (1-5)";
            this.MassegeThankyou = "Thank you for using our services. We hope to service you again in the future.";

            //option
            this.Product1 = "Office 365";
            this.Product2 = "Microsoft SQL Server";
            this.Product3 = "Microsoft Exchange";

            this.Priority1 = "High";
            this.Priority2 = "Very High";

            this.Priorityth1 = "สูง";
            this.Priorityth2 = "สูงมาก";

            this.IssueFrom1 = "1. Input text";
            this.IssueFrom2 = "2. Upload file";
            this.IssueFromth1 = "1. เพิ่มข้อความ";
            this.IssueFromth2 = "2. เพิ่มเอกสารแนบ";
            

            //User info
            this.Username = "Customer001" ;
            this.Password =  "123456";
            this.Incidents = "6";
            this.Expire = "31-12-2018" ;


            //Rating Star
            this.ImageRate1Url1 = "★";
            this.ImageRate1Url2 = "★★";
            this.ImageRate1Url3 = "★★★";
            this.ImageRate1Url4 = "★★★★";
            this.ImageRate1Url5 = "★★★★★";




        }

    }
}