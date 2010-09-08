// Copyright 2007-2010 The Apache Software Foundation.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace bigtoe
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("REL:{Name}:{EntityType.Name}:{With.Name}")]
    public class Relationship :
        Metadata
    {
        //name
        //from
        //to?
        public Relationship(string name, EntityType type) : base(name, type)
        {
        }

        public Metadata With { get; set; }

        public override string ToString()
        {
            return "Relationship: " + With.Name;
        }

        public static Relationship BuildHasA(EntityType type, Metadata with)
        {
            return new Relationship("has a", type)
                       {
                           With = with
                       };
        }

        public static Relationship BuildAssembly(Type type)
        {
            return new Relationship("contained in", EntityType.Assembly)
                       {
                           With = new Metadata(type.Assembly.GetName().Name, new EntityType("dotnet assembly"))
                       };
        }
    }
}