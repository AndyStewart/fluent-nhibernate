using System;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.AutoMap
{
    public class ManyToManyAutoMapper : IAutoMapper
    {
        private readonly AutoMappingExpressions conventions;

        public ManyToManyAutoMapper(AutoMappingExpressions conventions)
        {
            this.conventions = conventions;
        }

        public bool MapsProperty(PropertyInfo property)
        {
            var type = property.PropertyType;
            if (type.Namespace != "Iesi.Collections.Generic" &&
                type.Namespace != "System.Collections.Generic")
                return false;

            var hasInverse = GetInverseProperty(property) != null;
            return hasInverse;
        }

        private static PropertyInfo GetInverseProperty(PropertyInfo property)
        {
            Type type = property.PropertyType;
            var inverseSide = type.GetGenericTypeDefinition()
                .MakeGenericType(property.DeclaringType);

            var argument = type.GetGenericArguments()[0];
            return argument.GetProperties()
                .Where(x => x.PropertyType == inverseSide)
                .FirstOrDefault();
        }

        public void Map<T>(AutoMap<T> classMap, PropertyInfo property)
        {
            if (!classMap.CanMapProperty(property))
                return;

            PropertyInfo inverseProperty = GetInverseProperty(property);
            Type parentSide = conventions.GetParentSideForManyToMany(property.DeclaringType, inverseProperty.DeclaringType);
            IManyToManyPart manyToManyPart = GetManyToManyPart(classMap, property);

            if (parentSide != property.DeclaringType)
                ApplyInverse(property, parentSide, manyToManyPart);
        }

        public void Map(ClassMapping classMap, PropertyInfo property)
        {
            
        }

        public void ApplyInverse(PropertyInfo property, Type parentSide, IManyToManyPart manyToManyPart)
        {
            manyToManyPart.Inverse();
        }

        public IManyToManyPart GetManyToManyPart<T>(AutoMap<T> classMap, PropertyInfo property)
        {
            var listType = property.PropertyType.GetGenericArguments()[0];
            return (IManyToManyPart)InvocationHelper.InvokeGenericMethodWithDynamicTypeArguments(
                classMap,
                x => x.HasManyToMany<object>(ExpressionBuilder.Create<T>(property)),
                new object[] { ExpressionBuilder.Create<T>(property) },
                listType);
        }
    }
}
