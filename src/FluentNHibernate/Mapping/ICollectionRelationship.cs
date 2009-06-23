using System;
using System.Reflection;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Mapping
{
    public interface ICollectionRelationship : IRelationship
    {
        bool IsMethodAccess { get; }
        MemberInfo Member { get; }
        string TableName { get; }
        /// <summary>
        /// Specify caching for this entity.
        /// </summary>
        ICache Cache { get; }
        ICollectionCascadeExpression Cascade { get; }
        
        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        ICollectionRelationship Not { get; }
        ICollectionRelationship LazyLoad();
        ICollectionRelationship Inverse();
        ICollectionRelationship AsSet();
        ICollectionRelationship AsBag();
        ICollectionRelationship AsList();
        ICollectionRelationship AsMap(string indexColumnName);
        ICollectionRelationship AsMap<TIndex>(string indexColumnName);
        ICollectionRelationship AsElement(string columnName);

        /// <summary>
        /// Sets the table name for this one-to-many.
        /// </summary>
        /// <param name="name">Table name</param>
        ICollectionRelationship WithTableName(string name);

        ICollectionRelationship ForeignKeyCascadeOnDelete();



        /// <summary>
        /// Sets the where clause for this one-to-many relationship.
        /// </summary>
        ICollectionRelationship Where(string where);

        ICollectionRelationship BatchSize(int size);

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        ICollectionRelationship CollectionType<TCollection>();

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        ICollectionRelationship CollectionType(Type type);

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        ICollectionRelationship CollectionType(string type);

        ICollectionMapping GetCollectionMapping();
    }
}