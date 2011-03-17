using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using bigtoe;
using Magnum.Extensions;

namespace toe
{
    using bigtoe.Configuration;

    class Program
    {
        /*
         * -assemblies:.\assemblies -file:.\assemblies.txt
         */
        static void Main(string[] args)
        {
            var binder = Magnum.Configuration.ConfigurationBinderFactory.New(o =>
            {
                o.AddCommandLine(args.Aggregate("", (a, b) => a + " " + b).Trim());
            });

            var cfg = binder.Bind<Config>();


            var messages = new List<Metadata>();
            var messagesByAssembly = new Dictionary<string, List<Metadata>>();
            var assemblies = File.ReadAllLines(cfg.File);

            var ass = new List<Assembly>();

            try
            {
                LoadAssemblies(assemblies, ass);

                foreach (var assembly in ass)
                {
                    if(!assembly.FullName.StartsWith("FHLBank"))
                        continue;

                    messagesByAssembly.Add(assembly.GetName().Name, new List<Metadata>());

                    ProcessAssembly(assembly, messagesByAssembly, messages);
                }

                messages = messages.OrderBy(m => m.Name).ToList();
                MetaViewer.BuildIndex(@".\template\index_template.html", messages, messagesByAssembly);
                MetaViewer.CopyImages();
            }
            catch (ReflectionTypeLoadException ex)
            {
                Console.WriteLine("Ka-Doom :( You Killed Big Toe");
                Console.WriteLine("Here is how I died:");
                Console.WriteLine("-----");
                ex.LoaderExceptions.Each(x => Console.WriteLine("  {0}", x.Message));
                Console.WriteLine("-----");
                Console.WriteLine("You earned {0} exp", new Random(5).Next(1, 50));
                Console.ReadKey(true);
            }
        }

        private static void ProcessAssembly(Assembly assembly, Dictionary<string, List<Metadata>> messagesByAssembly, List<Metadata> msg)
        {
            try
            {

            assembly.GetTypes().Each(t =>
                                         {
                                             if (!t.Name.Contains("Message")) return;

                                             var model = MetaModel.BuildMessage(t);
                                             msg.Add(model);
                                             messagesByAssembly[assembly.GetName().Name].Add(model);

                                             MetaViewer.DumpToHtml(@".\template\message_template.html", model);
                                         });
            }
            catch (ReflectionTypeLoadException ex)
            {
                Console.WriteLine("Processing Assembly: '{0}'", assembly.GetName().Name);
                ex.LoaderExceptions.Each(x => Console.WriteLine(x.Message));
            }
        }

        private static void LoadAssemblies(string[] assemblies, List<Assembly> a)
        {
            Assembly.ReflectionOnlyLoad("System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Assembly.ReflectionOnlyLoad(typeof(Microsoft.VisualBasic.ComClassAttribute).Assembly.FullName);

            foreach(var assm in assemblies)
            {
                try
                {
                    var ass = Assembly.ReflectionOnlyLoadFrom(Path.Combine(@".\assemblies", assm));
                    a.Add(ass);
                }
                catch (ReflectionTypeLoadException ex)
                {
                    ex.LoaderExceptions.Each(x => Console.WriteLine(ex.Message));
                    Console.WriteLine("Error loading: '{0}'", assm);
                }               
            }
        }
    }


}
