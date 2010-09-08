namespace bigtoe.specs
{
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

    public class Address
    {
        public string StreetNumber { get; set; }
    }
}