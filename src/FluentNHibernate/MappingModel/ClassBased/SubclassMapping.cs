﻿using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class SubclassMapping : ClassMappingBase, ISubclassMapping
    {
        private readonly AttributeStore<SubclassMapping> attributes;
        private readonly IList<SubclassMapping> subclasses;

        public SubclassMapping()
            : this(new AttributeStore())
        { }

        protected SubclassMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            attributes = new AttributeStore<SubclassMapping>(underlyingStore);
            subclasses = new List<SubclassMapping>();
        }

        public AttributeStore<SubclassMapping> Attributes
        {
            get { return attributes; }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessSubclass(this);

            foreach(var subclass in Subclasses)
                visitor.Visit(subclass);

            base.AcceptVisitor(visitor);
        }

        public void AddSubclass(SubclassMapping subclassMapping)
        {
            subclasses.Add(subclassMapping);
        }

        public IEnumerable<SubclassMapping> Subclasses
        {
            get { return subclasses; }
        }

        public object DiscriminatorValue
        {
            get { return attributes.Get(x => x.DiscriminatorValue); }
            set { attributes.Set(x => x.DiscriminatorValue, value); }
        }

        public string Extends
        {
            get { return attributes.Get(x => x.Extends); }
            set { attributes.Set(x => x.Extends, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get(x => x.Lazy); }
            set { attributes.Set(x => x.Lazy, value); }
        }

        public string Proxy
        {
            get { return attributes.Get(x => x.Proxy); }
            set { attributes.Set(x => x.Proxy, value); }
        }

        public bool DynamicUpdate
        {
            get { return attributes.Get(x => x.DynamicUpdate); }
            set { attributes.Set(x => x.DynamicUpdate, value); }
        }

        public bool DynamicInsert
        {
            get { return attributes.Get(x => x.DynamicInsert); }
            set { attributes.Set(x => x.DynamicInsert, value); }
        }

        public bool SelectBeforeUpdate
        {
            get { return attributes.Get(x => x.SelectBeforeUpdate); }
            set { attributes.Set(x => x.SelectBeforeUpdate, value); }
        }

        public bool Abstract
        {
            get { return attributes.Get(x => x.Abstract); }
            set { attributes.Set(x => x.Abstract, value); }
        }
    }
}