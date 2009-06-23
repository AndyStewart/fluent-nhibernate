using System.Linq;
using System.Reflection;
using FluentNHibernate.Conventions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.AutoMap
{
    public class AutoMapColumn : IAutoMapper
    {
        private readonly IConventionFinder conventionFinder;

        public AutoMapColumn(IConventionFinder conventionFinder)
        {
            this.conventionFinder = conventionFinder;
        }

        public bool MapsProperty(PropertyInfo property)
        {
            if (HasExplicitTypeConvention(property))
                return true;

            if (property.CanWrite)
                return IsMappableToColumnType(property);

            return false;
        }

        private bool HasExplicitTypeConvention(PropertyInfo property)
        {
            var conventions = conventionFinder
                .Find<IUserTypeConvention>()
                .Where(c => c.Accept(property.PropertyType));

            return conventions.FirstOrDefault() != null;
        }

        private static bool IsMappableToColumnType(PropertyInfo property)
        {
            return property.PropertyType.Namespace == "System"
                   || property.PropertyType.FullName == "System.Drawing.Bitmap";
        }

        public void Map<T>(AutoMap<T> classMap, PropertyInfo property)
        {
        }

        public void Map(ClassMapping classMap, PropertyInfo property)
        {
            if (property.DeclaringType != classMap.Type)
                return;

            var propertyMap = new PropertyMapping();
            propertyMap.AddColumn(new ColumnMapping() { Name = property.Name });
            propertyMap.Name = property.Name;
            classMap.AddProperty(propertyMap);
        }
    }
}