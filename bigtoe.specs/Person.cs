namespace bigtoe.specs
{
    using System;

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }

        //nullable test
        public int? Xxx { get; set; }

        //method test
        public void Bob(){}
    }

    [Obsolete("Message")]
    public class Address
    {
        [Obsolete("Message2")]
        public string StreetNumber { get; set; }
    }
}