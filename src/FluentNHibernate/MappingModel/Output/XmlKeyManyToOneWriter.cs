using System.Xml;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlKeyManyToOneWriter : NullMappingModelVisitor, IXmlWriter<KeyManyToOneMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;
        private XmlDocument document;

        public XmlKeyManyToOneWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(KeyManyToOneMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessKeyManyToOne(KeyManyToOneMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement("key-many-to-one");

            if (mapping.Attributes.IsSpecified(x => x.Access))
                element.WithAtt("access", mapping.Access);

            if (mapping.Attributes.IsSpecified(x => x.Name))
                element.WithAtt("name", mapping.Name);

            if (mapping.Attributes.IsSpecified(x => x.Class))
                element.WithAtt("class", mapping.Class);

            if (mapping.Attributes.IsSpecified(x => x.ForeignKey))
                element.WithAtt("foreign-key", mapping.ForeignKey);

            if (mapping.Attributes.IsSpecified(x => x.Lazy))
                element.WithAtt("lazy", mapping.Lazy);

            if (mapping.Attributes.IsSpecified(x => x.NotFound))
                element.WithAtt("not-found", mapping.NotFound);
        }

        public override void Visit(ColumnMapping columnMapping)
        {
            var writer = serviceLocator.GetWriter<ColumnMapping>();
            var xml = writer.Write(columnMapping);

            document.ImportAndAppendChild(xml);
        }
    }
}