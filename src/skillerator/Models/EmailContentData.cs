namespace skillerator.Models{
    public class EmailContentData{
        public string To {get; set; }
        public string Subject {get; set;}
        public string Body {get; set;}
        public EmailContentData(string to, string subject, string body){
            this.To = to;
            this.Subject = subject;
            this.Body = body;
        }
    }
}