﻿using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.MappingModel
{
    public class DiscriminatorMapping : MappingBase
    {
        private readonly AttributeStore<DiscriminatorMapping> attributes;
        public ClassMapping ParentClass { get; internal set; }

        public DiscriminatorMapping(ClassMapping parentClass)
        {
            ParentClass = parentClass;

            attributes = new AttributeStore<DiscriminatorMapping>();
            attributes.SetDefault(x => x.NotNull, true);
            attributes.SetDefault(x => x.Insert, true);
            attributes.SetDefault(x => x.Type, typeof(string));
            
        }

        public AttributeStore<DiscriminatorMapping> Attributes
        {
            get { return attributes; }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessDiscriminator(this);
        }

        public string ColumnName
        {
            get { return attributes.Get(x => x.ColumnName); }
            set { attributes.Set(x => x.ColumnName, value); }
        }

        public bool NotNull
        {
            get { return attributes.Get(x => x.NotNull); }
            set { attributes.Set(x => x.NotNull, value); }
        }

        public int Length
        {
            get { return attributes.Get(x => x.Length); }
            set { attributes.Set(x => x.Length, value); }
        }

        public bool Force
        {
            get { return attributes.Get(x => x.Force); }
            set { attributes.Set(x => x.Force, value); }
        }

        public bool Insert
        {
            get { return attributes.Get(x => x.Insert); }
            set { attributes.Set(x => x.Insert, value); }
        }

        public string Formula
        {
            get { return attributes.Get(x => x.Formula); }
            set { attributes.Set(x => x.Formula, value); }
        }

        public Type Type
        {
            get { return attributes.Get(x => x.Type); }
            set { attributes.Set(x => x.Type, value); }
        }
    }
}
