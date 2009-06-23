using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Output;
using FluentNHibernate.Testing.Testing;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class XmlAnyWriterTester
    {
        private IXmlWriter<AnyMapping> writer;

        [SetUp]
        public void GetWriterFromContainer()
        {
            var container = new XmlWriterContainer();
            writer = container.Resolve<IXmlWriter<AnyMapping>>();
        }

        [Test]
        public void ShouldWriteIdTypeAttribute()
        {
            var testHelper = new XmlWriterTestHelper<AnyMapping>();
            testHelper.Check(x => x.IdType, "id").MapsToAttribute("id-type");

            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteMetaTypeAttribute()
        {
            var testHelper = new XmlWriterTestHelper<AnyMapping>();
            testHelper.Check(x => x.MetaType, "meta").MapsToAttribute("meta-type");

            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteNameAttribute()
        {
            var testHelper = new XmlWriterTestHelper<AnyMapping>();
            testHelper.Check(x => x.Name, "name").MapsToAttribute("name");

            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteAccessAttribute()
        {
            var testHelper = new XmlWriterTestHelper<AnyMapping>();
            testHelper.Check(x => x.Access, "acc").MapsToAttribute("access");

            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteInsertAttribute()
        {
            var testHelper = new XmlWriterTestHelper<AnyMapping>();
            testHelper.Check(x => x.Insert, true).MapsToAttribute("insert");

            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteUpdateAttribute()
        {
            var testHelper = new XmlWriterTestHelper<AnyMapping>();
            testHelper.Check(x => x.Update, true).MapsToAttribute("update");

            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteCascadeAttribute()
        {
            var testHelper = new XmlWriterTestHelper<AnyMapping>();
            testHelper.Check(x => x.Cascade, "all").MapsToAttribute("cascade");

            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteColumns()
        {
            var mapping = new AnyMapping();

            mapping.AddColumn(new ColumnMapping { Name = "Column1" });

            writer.VerifyXml(mapping)
                .Element("column").Exists();
        }

        [Test]
        public void ShouldWriteMetaValues()
        {
            var mapping = new AnyMapping();

            mapping.AddMetaValue(new MetaValueMapping());

            writer.VerifyXml(mapping)
                .Element("meta-value").Exists();
        }
    }
}