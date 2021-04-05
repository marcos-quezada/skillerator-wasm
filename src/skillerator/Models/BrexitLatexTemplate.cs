namespace skillerator.Models{
    public class BrexitLatexTemplate{
        public CompileElement Compile {get; set;}

        public BrexitLatexTemplate(CompileElement compile){
            this.Compile = compile;
        }
    }

    public class CompileElement{
        public Options Options {get; set;}
        public Resource[] Resources {get; set;} 

        public CompileElement(Options options, Resource[] resources){
            this.Options = options;
            this.Resources = resources;
        }
    }

    public class Options{
        public string Compiler {get; set;}
        public int Timeout {get; set;}

        public Options(string compiler, int timeout){
            this.Compiler = compiler;
            this.Timeout = timeout;
        }
        
    }

    public class Resource{
        public string Path {get; set;}
        public string Content {get; set;}

        public Resource(string path, string content){
            this.Path = path;
            this.Content = content;
        }
    }

}