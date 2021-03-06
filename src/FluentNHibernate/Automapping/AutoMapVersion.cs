using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoMapVersion : IAutoMapper
    {
        private static readonly IList<string> ValidNames = new List<string> { "version", "timestamp" };
        private static readonly IList<Type> ValidTypes = new List<Type> { typeof(int), typeof(long), typeof(DateTime), typeof(TimeSpan), typeof(byte[]) };

        public bool MapsProperty(Member property)
        {
            return ValidNames.Contains(property.Name.ToLowerInvariant()) && ValidTypes.Contains(property.PropertyType);
        }

        public void Map(ClassMappingBase classMap, Member property)
        {
            if (!(classMap is ClassMapping)) return;

            var version = new VersionMapping
            {
                Name = property.Name,
            };

            version.SetDefaultValue("Type", GetDefaultType(property));
            version.AddDefaultColumn(new ColumnMapping { Name = property.Name });

            if (IsSqlTimestamp(property))
            {
                version.Columns.Each(x =>
                {
                    x.SqlType = "timestamp";
                    x.NotNull = true;
                });
                version.UnsavedValue = null;
            }

            ((ClassMapping)classMap).Version = version;
        }

        private bool IsSqlTimestamp(Member property)
        {
            return property.PropertyType == typeof(byte[]);
        }

        private TypeReference GetDefaultType(Member property)
        {
            if (property.PropertyType == typeof(DateTime))
                return new TypeReference("Timestamp");

            if (IsSqlTimestamp(property))
                return new TypeReference("BinaryBlob");

            return new TypeReference(property.PropertyType);
        }
    }
}