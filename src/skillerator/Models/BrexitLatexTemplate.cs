using System.Text.Json.Serialization;

namespace skillerator.Models{
    public class BrexitLatexTemplate{
        [JsonPropertyName("compile")]
        public CompileElement Compile {get; set;}

        public BrexitLatexTemplate(CompileElement compile){
            this.Compile = compile;
        }
    }

    public class CompileElement{
        [JsonPropertyName("options")]
        public Options Options {get; set;}

        [JsonPropertyName("resources")]
        public Resource[] Resources {get; set;} 

        public CompileElement(Options options, Resource[] resources){
            this.Options = options;
            this.Resources = resources;
        }
    }

    public class Options{
        [JsonPropertyName("compiler")]
        public string Compiler {get; set;}
        
        [JsonPropertyName("timeout")]
        public int Timeout {get; set;}

        public Options(string compiler, int timeout){
            this.Compiler = compiler;
            this.Timeout = timeout;
        }
        
    }

    public class Resource{
        [JsonPropertyName("path")]
        public string Path {get; set;}
        
        [JsonPropertyName("content")]
        public string Content {get; set;}

        public Resource(string path, string content){
            this.Path = path;
            this.Content = content;
        }
    }

}