﻿using System.Linq;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Testing.DomainModel;
using Machine.Specifications;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    public class when_class_map_is_told_to_create_an_inline_subclass : ProviderSpec
    {
        // ignored warning for obsolete SubClass
#pragma warning disable 612,618

        Because of = () =>
            class_mapping = map_as_class<SuperRecord>(m => m.DiscriminateSubClassesOnColumn("col").SubClass<ChildRecord>(sc => { }));

#pragma warning restore 612,618

        It should_add_subclass_to_class_mapping_subclasses_collection = () =>
            class_mapping.Subclasses.Count().ShouldEqual(1);

        static ClassMapping class_mapping;
    }

    public class when_class_map_is_told_to_create_an_inline_joined_subclass : ProviderSpec
    {
        // ignored warning for obsolete JoinedSubClass
#pragma warning disable 612,618

        Because of = () =>
            class_mapping = map_as_class<SuperRecord>(m => m.JoinedSubClass<ChildRecord>("key", c => { }));

#pragma warning restore 612,618

        It should_add_joined_subclass_to_class_mapping_subclasses_collection = () =>
            class_mapping.Subclasses.Count().ShouldEqual(1);

        It should_create_a_key_for_the_subclass = () =>
            ((JoinedSubclassMapping)class_mapping.Subclasses.First()).Key.ShouldNotBeNull();

        It should_create_a_column_for_the_key_with_the_name_specified = () =>
            ((JoinedSubclassMapping)class_mapping.Subclasses.First()).Key.Columns.Single().Name.ShouldEqual("key");

        static ClassMapping class_mapping;
    }
}