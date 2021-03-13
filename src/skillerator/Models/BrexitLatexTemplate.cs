namespace skillerator.Models{
    public class BrexitLatexTemplate{
        public CompileElement compile {get; set;}

        public BrexitLatexTemplate(CompileElement compile){
            this.compile = compile;
        }
    }

    public class CompileElement{
        public Options options {get; set;}
        public Resource[] resources {get; set;} 

        public CompileElement(Options options, Resource[] resources){
            this.options = options;
            this.resources = resources;
        }
    }

    public class Options{
        public string compiler {get; set;}
        public int timeout {get; set;}

        public Options(string compiler, int timeout){
            this.compiler = compiler;
            this.timeout = timeout;
        }
        
    }

    public class Resource{
        public string path {get; set;}
        public string content {get; set;}

        public Resource(string path, string content){
            this.path = path;
            this.content = content;
        }
    }

}