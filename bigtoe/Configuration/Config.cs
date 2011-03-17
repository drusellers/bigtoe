namespace bigtoe.Configuration
{
    public class Config
    {
        public string[] Dlls { get; set; }
        public string File { get; set; } //assemblies to actually use
        public string AssembliesPath { get; set; } //all assemblies needed
    }
}