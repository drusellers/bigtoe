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
                    p => p.Name == "Xxx");
            Assert.IsNotNull(props);
            Assert.IsTrue(props.IsNullable());
        }

        [Then]
        public void Bob()
        {
            try
            {
                Assembly.ReflectionOnlyLoad(typeof(Microsoft.VisualBasic.ComClassAttribute).Assembly.FullName);
                Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\MassTransit.ServiceBus.dll");
                var b1 = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Derivatives.dll");
                var b = Assembly.ReflectionOnlyLoadFrom(@"D:\Development\git-bigtoe\FHLBank.Shared.Messages.Debt.dll");
                var messages = new List<Metadata>();
                b.GetTypes().Each(t =>
                {
                    if (!t.Name.EndsWith("Message")) return;

                    var model = MetaModel.BuildMessage(t);
                    messages.Add(model);
                    MetaViewer.DumpToHtml("message_template.html", model);
                });
                b1.GetTypes().Each(t =>
                {
                    if (!t.Name.EndsWith("Message")) return;

                    var model = MetaModel.BuildMessage(t);
                    messages.Add(model);
                    MetaViewer.DumpToHtml("message_template.html", model);
                });


                MetaViewer.BuildIndex("index_template.html", messages);
            }
            catch (ReflectionTypeLoadException ex)
            {

                throw;
            }

        }


    }


}