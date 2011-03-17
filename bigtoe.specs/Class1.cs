namespace bigtoe.specs
{
    using System;
    using System.Linq;

    public class Class1
    {
        public void Bob()
        {
            var x = (from c in "12345"
                    where c.Equals('4') || c.Equals('5')
                    select c)
                    .Count() > 1;

            if (x)
                Console.WriteLine("Hi");
        }
    }
}