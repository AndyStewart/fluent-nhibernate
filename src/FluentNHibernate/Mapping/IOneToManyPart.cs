using System;
using FluentNHibernate.MappingModel.Collections;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public interface IOneToManyPart : ICollectionRelationship
    {
        CollectionCascadeExpression<IOneToManyPart> Cascade { get; }
        IOneToManyPart Inverse();
        IOneToManyPart LazyLoad();
        INotFoundExpression NotFound { get; }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        IOneToManyPart CollectionType<TCollection>();

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        IOneToManyPart CollectionType(Type type);

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        IOneToManyPart CollectionType(string type);

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        IOneToManyPart Not { get; }
        IOneToManyPart KeyColumnName(string columnName);
        IColumnNameCollection KeyColumnNames { get; }
        OuterJoinBuilder<IOneToManyPart> OuterJoin { get; }
        FetchTypeExpression<IOneToManyPart> Fetch { get; }
        OptimisticLockBuilder<IOneToManyPart> OptimisticLock { get; }
        IOneToManyPart SchemaIs(string schema);
        ICollectionMapping GetCollectionMapping();
        IOneToManyPart Persister<T>() where T : IEntityPersister;
        IOneToManyPart Check(string checkSql);
        IOneToManyPart Generic();
        IOneToManyPart WithForeignKeyConstraintName(string foreignKeyName);
    }
}
