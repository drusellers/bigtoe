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
           

        }


    }


}