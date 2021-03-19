namespace skillerator.Models{
    public class EmailContentData{
        public string to {get; set; }
        public string subject {get; set;}
        public string body {get; set;}
        public EmailContentData(string to, string subject, string body){
            this.to = to;
            this.subject = subject;
            this.body = body;
        }
    }
}