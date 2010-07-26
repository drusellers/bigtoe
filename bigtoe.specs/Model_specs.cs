namespace bigtoe.specs
{
    using System;
    using Magnum.TestFramework;
    using NUnit.Framework;

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
            _model = MetaModel.Build(typeof(Person));
        }

        [Then]
        public void Some()
        {
            MetaViewer.View(_model);
        }


    }


}