namespace bigtoe.specs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Magnum.TestFramework;
    using NUnit.Framework;
    using Magnum.Extensions;

    public class Given_A_Class_To_Process
    {
        public Type TypeToMetaModel = typeof (Person);
    }

    [TestFixture]
    public class Model_specs
    {
        Metadata _model;

        [When]
        public void BuildAModel()
        {
            _model = MetaModel.BuildClass(typeof(Person));
        }

        [Then]
        public void Some()
        {
            MetaViewer.View(_model);
        }

        [Then]
        public void NullTest()
        {
            var props =
                _model.Relationships.Where(r => r.EntityType == EntityType.Property).Select(r => r.With).First(
                    p => p.Name == "Name");
            Assert.IsNotNull(props);
            Assert.IsTrue(props.IsNullable());
        }

        [Then]
        public void Bob()
        {
            try
            {
                Assembly.ReflectionOnlyLoad("System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                Assembly.ReflectionOnlyLoad(typeof(Microsoft.VisualBasic.ComClassAttribute).Assembly.FullName);
                Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\MassTransit.ServiceBus.dll");
                var b0 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Derivatives.dll");
                var b1 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Debt.dll");
                var b2 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Entities.dll");
                var b3 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Advances.dll");
                var b4 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Deposits.dll");
                var b5 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.dll");
                var b6 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.GeneralLedger.dll");
                var b7 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Investments.dll");
                var b8 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Safekeeping.dll");

                var assemblies = new List<Assembly>
                                     {
                                         b0,
                                         b1,
                                         b2,
                                         b3,
                                         b4,
                                         b5,
                                         b6,
                                         b7,
                                         b8
                                     };

                var messages = new List<Metadata>();
                var messagesByAssembly = new Dictionary<string, List<Metadata>>();

                foreach (var assembly in assemblies)
                {
                    messagesByAssembly.Add(assembly.GetName().Name, new List<Metadata>());

                    var msg = messages;
                    var ass = assembly;

                    assembly.GetTypes().Each(t =>
                    {
                        if (!t.Name.Contains("Message")) return;

                        var model = MetaModel.BuildMessage(t);
                        msg.Add(model);
                        messagesByAssembly[ass.GetName().Name].Add(model);

                        MetaViewer.DumpToHtml("message_template.html", model);
                    });
                }

                messages = messages.OrderBy(m => m.Name).ToList();
                MetaViewer.BuildIndex("index_template.html", messages, messagesByAssembly);
            }
            catch (ReflectionTypeLoadException ex)
            {

                throw;
            }

        }


    }


}