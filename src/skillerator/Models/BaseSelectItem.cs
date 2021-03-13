namespace skillerator.Models{
    public class BaseSelectItem{
        public int Id {get; set;}
        public string Text {get; set;}
        public string Value {get; set;}

        public BaseSelectItem(string text){
            Text = text;
        }
    }
}