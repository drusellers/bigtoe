namespace bigtoe
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Magnum.Extensions;
    using Spark;
    using Spark.FileSystem;

    public class MetaViewer
    {
        public static void CopyImages()
        {
            var files = Directory.GetFiles(@".\template", "*.png");
            files.Each(f => File.Copy(f, Path.Combine(@".\output", Path.GetFileName(f)), true));
        }
        public static void View(Metadata data)
        {
            Console.WriteLine("a '{0}' named '{1}' ", data.EntityType.Name, data.Name);
            foreach (var rel in data.Relationships)
            {
                Console.WriteLine("  '{0}' '{1}' '{2}' is a '{3}'", rel.Name,rel.EntityType.Name, rel.With.Name, rel.With.EntityType.Name);
                foreach (var r in rel.With.Relationships)
                {
                    Console.WriteLine("    '{0}' '{1}' '{2}' '{3}'", r.Name, r.EntityType.Name, r.With.Name, r.With.EntityType.Name);
                }
            }
        }
        public static void DumpToFile(Metadata data)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("a '{0}' named '{1}' ", data.EntityType.Name, data.Name);
            foreach (var rel in data.Relationships)
            {
                sb.AppendLine();
                sb.AppendFormat("  '{0}' '{1}' '{2}' is a '{3}'", rel.Name, rel.EntityType.Name, rel.With.Name, rel.With.EntityType.Name);
                foreach (var r in rel.With.Relationships)
                {
                    sb.AppendLine();
                    sb.AppendFormat("    '{0}' '{1}' '{2}' '{3}'", r.Name, r.EntityType.Name, r.With.Name, r.With.EntityType.Name);
                }
            }
            File.WriteAllText("{0}.model.txt".FormatWith(data.Name),sb.ToString());
        }

        public static void BuildIndex(string template, List<Metadata> model, Dictionary<string, List<Metadata>> byAssembly)
        {
            var viewFolder = new FileSystemViewFolder(".");
            var engine = new SparkViewEngine()
            {
                DefaultPageBaseType = typeof(IndexView).FullName,
                ViewFolder = viewFolder
            };

            string templateResult = ProcessViewTemplate(engine, template, model, byAssembly);

            if (!Directory.Exists("output")) Directory.CreateDirectory("output");
            File.WriteAllText(".\\output\\index.html", templateResult);
        }
        public static void DumpToHtml(string template, Metadata model)
        {
            var viewFolder = new FileSystemViewFolder(".");
            var engine = new SparkViewEngine()
                             {
                                 DefaultPageBaseType = typeof (MessageView).FullName,
                                 ViewFolder = viewFolder
                             };
            string templateResult = ProcessViewTemplate(engine, template, model);

            if (!Directory.Exists("output")) Directory.CreateDirectory("output");
            File.WriteAllText(".\\output\\{0}.model.html".FormatWith(model.Name), templateResult);
        }

        static string ProcessViewTemplate(SparkViewEngine engine, string templateName, Metadata model)
        {
            var view = (MessageView)engine.CreateInstance(
                new SparkViewDescriptor()
                    .AddTemplate(templateName));

            view.Model = model;

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                view.RenderView(writer);
            }

            return sb.ToString();
        }

        static string ProcessViewTemplate(SparkViewEngine engine, string templateName, List<Metadata> model, Dictionary<string, List<Metadata>> byAssembly)
        {
            var view = (IndexView)engine.CreateInstance(
                new SparkViewDescriptor()
                    .AddTemplate(templateName));

            view.Model = new ComplexView()
                             {
                                 ByAssembly = byAssembly,
                                 ByMessage = model
                             };


            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                view.RenderView(writer);
            }

            return sb.ToString();
        }

    }

    public abstract class IndexView :
        AbstractSparkView
    {
        public ComplexView Model { get; set; }
    }
    public abstract class MessageView : 
        AbstractSparkView
    {
        public Metadata Model { get; set; }
    }
    public class ComplexView
    {
        public List<Metadata> ByMessage { get; set; }
        public Dictionary<string, List<Metadata>> ByAssembly { get; set; }

    }

}