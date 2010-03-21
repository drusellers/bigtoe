namespace bigtoe.specs
{
    using NUnit.Framework;

    [TestFixture]
    public class Model_specs
    {
        [Test]
        public void BuildAModel()
        {
            var model = MetaModel.Build<Person>();
            MetaViewer.View(model);
        }
    }
}