using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.DomainModel.Mapping;
using FluentNHibernate.Utils;
using Iesi.Collections.Generic;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    public abstract class BaseModelFixture
    {
        protected ModelTester<ClassMap<T>, ClassMapping> ClassMap<T>()
        {
            return new ModelTester<ClassMap<T>, ClassMapping>(() => new ClassMap<T>(), x => x.GetClassMapping());
        }

        protected ModelTester<DiscriminatorPart, DiscriminatorMapping> DiscriminatorMap<T>()
        {
            return new ModelTester<DiscriminatorPart, DiscriminatorMapping>(() =>
            {
                var classMapping = new ClassMapping();
                var classMap = new ClassMap<T>(classMapping);
                return new DiscriminatorPart(classMap, classMapping, "column");
            }, x => x.GetDiscriminatorMapping());
        }

        protected ModelTester<SubClassPart<T>, SubclassMapping> SubClass<T>()
        {
            return new ModelTester<SubClassPart<T>, SubclassMapping>(() => new SubClassPart<T>(new SubclassMapping()), x => x.GetSubclassMapping());
        }

        protected ModelTester<JoinedSubClassPart<T>, JoinedSubclassMapping> JoinedSubClass<T>()
        {
            return new ModelTester<JoinedSubClassPart<T>, JoinedSubclassMapping>(() => new JoinedSubClassPart<T>(new JoinedSubclassMapping()), x => x.GetJoinedSubclassMapping());
        }

        protected ModelTester<ComponentPart<T>, ComponentMapping> Component<T>()
        {
            return new ModelTester<ComponentPart<T>, ComponentMapping>(() => new ComponentPart<T>(new ComponentMapping(), "prop"), x => (ComponentMapping)((IComponent)x).GetComponentMapping());
        }

        protected ModelTester<DynamicComponentPart<T>, DynamicComponentMapping> DynamicComponent<T>()
        {
            return new ModelTester<DynamicComponentPart<T>, DynamicComponentMapping>(() => new DynamicComponentPart<T>(new DynamicComponentMapping(), "prop"), x => (DynamicComponentMapping)((IDynamicComponent)x).GetComponentMapping());
        }

        protected ModelTester<IVersion, VersionMapping> Version()
        {
            return new ModelTester<IVersion, VersionMapping>(() => new VersionPart(typeof(VersionTarget), ReflectionHelper.GetProperty<VersionTarget>(x => x.VersionNumber)), x => x.GetVersionMapping());
        }

        protected ModelTester<ICache, CacheMapping> Cache()
        {
            return new ModelTester<ICache, CacheMapping>(() => new CachePart(), x => x.GetCacheMapping());
        }

        protected ModelTester<IIdentityPart, IdMapping> Id()
        {
            return new ModelTester<IIdentityPart, IdMapping>(() => new IdentityPart(typeof(IdentityTarget), ReflectionHelper.GetProperty<IdentityTarget>(x => x.IntId)), x => x.GetIdMapping());
        }

        protected ModelTester<CompositeIdentityPart<T>, CompositeIdMapping> CompositeId<T>()
        {
            return new ModelTester<CompositeIdentityPart<T>, CompositeIdMapping>(() => new CompositeIdentityPart<T>(), x => ((ICompositeIdMappingProvider)x).GetCompositeIdMapping());
        }

        protected ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping> OneToOne()
        {
            return new ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping>(() => new OneToOnePart<PropertyReferenceTarget>(typeof(PropertyTarget), ReflectionHelper.GetProperty<PropertyTarget>(x => x.Reference)), x => x.GetOneToOneMapping());
        }

        protected ModelTester<IProperty, PropertyMapping> Property()
        {
            return new ModelTester<IProperty, PropertyMapping>(() => new PropertyMap(ReflectionHelper.GetProperty<PropertyTarget>(x => x.Name), typeof(PropertyTarget)), x => x.GetPropertyMapping());
        }

        protected ModelTester<OneToManyPart<T>, ICollectionMapping> OneToMany<T>(Expression<Func<OneToManyTarget, IList<T>>> property)
        {
            return new ModelTester<OneToManyPart<T>, ICollectionMapping>(() => new OneToManyPart<T>(typeof(OneToManyTarget), ReflectionHelper.GetProperty(property)), x => x.GetCollectionMapping());
        }

        protected ModelTester<OneToManyPart<T>, ICollectionMapping> OneToMany<T>(Expression<Func<OneToManyTarget, ISet<T>>> property)
        {
            return new ModelTester<OneToManyPart<T>, ICollectionMapping>(() => new OneToManyPart<T>(typeof(OneToManyTarget), ReflectionHelper.GetProperty(property)), x => x.GetCollectionMapping());
        }

        protected ModelTester<OneToManyPart<T>, ICollectionMapping> OneToMany<T>(Expression<Func<OneToManyTarget, IDictionary<string, T>>> property)
        {
            return new ModelTester<OneToManyPart<T>, ICollectionMapping>(() => new OneToManyPart<T>(typeof(OneToManyTarget), ReflectionHelper.GetProperty(property)), x => x.GetCollectionMapping());
        }

        protected ModelTester<ManyToManyPart<T>, ICollectionMapping> ManyToMany<T>(Expression<Func<ManyToManyTarget, IList<T>>> property)
        {
            return new ModelTester<ManyToManyPart<T>, ICollectionMapping>(() => new ManyToManyPart<T>(typeof(ManyToManyTarget), ReflectionHelper.GetProperty(property)), x => x.GetCollectionMapping());
        }

        protected ModelTester<IManyToOnePart, ManyToOneMapping> ManyToOne()
        {
            return new ModelTester<IManyToOnePart, ManyToOneMapping>(() => new ManyToOnePart<PropertyReferenceTarget>(typeof(PropertyTarget), ReflectionHelper.GetProperty<PropertyTarget>(x => x.Reference)), x => x.GetManyToOneMapping());
        }

        protected ModelTester<AnyPart<T>, AnyMapping> Any<T>()
        {
            return new ModelTester<AnyPart<T>, AnyMapping>(() => new AnyPart<T>(ReflectionHelper.GetProperty<MappedObject>(x => x.Parent)), x => ((IAnyMappingProvider)x).GetAnyMapping());
        }

        protected ModelTester<JoinPart<T>, JoinMapping> Join<T>(string table)
        {
            return new ModelTester<JoinPart<T>, JoinMapping>(() => new JoinPart<T>(table), x => x.GetJoinMapping());
        }

        protected ModelTester<HibernateMappingPart, HibernateMapping> HibernateMapping()
        {
            return new ModelTester<HibernateMappingPart, HibernateMapping>(() => new HibernateMappingPart(), x => ((IHibernateMappingProvider)x).GetHibernateMapping());
        }

        protected ModelTester<CompositeElementPart<T>, CompositeElementMapping> CompositeElement<T>()
        {
            return new ModelTester<CompositeElementPart<T>, CompositeElementMapping>(() => new CompositeElementPart<T>(), x => ((ICompositeElementMappingProvider)x).GetCompositeElementMapping());
        }
    }
}