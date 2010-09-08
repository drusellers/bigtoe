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
    using System.Diagnostics;

    [DebuggerDisplay("{Name}")]
    public class EntityType :
        Meta
    {
        public static EntityType Null = new EntityType("null");
        public static EntityType Property = new EntityType("Property");
        public static EntityType Method = new EntityType("Method");
        public static EntityType Parameter = new EntityType("Parameter");
        public static EntityType Class = new EntityType("Class");
        public static EntityType Message = new EntityType("Message");
        public static EntityType Constructor = new EntityType("Constructor");
        public static EntityType Note = new EntityType("Note");
        public static EntityType Assembly = new EntityType("Assembly");

        public EntityType(string name)
        {
            Name = name;
        }


        public override string ToString()
        {
            return "EntityType: " + Name;
        }
    }
}