using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridApp.DataModel
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        private static readonly string[] firstNames = { "Adam", "Bob", "Carl", "David", "Edgar", "Frank", "George", "Harry", "Isaac", "Jesse", "Ken", "Larry" };
        private static readonly string[] lastNames = { "Aaronson", "Bobson", "Carlson", "Davidson", "Enstwhile", "Ferguson", "Harrison", "Isaacson", "Jackson", "Kennelworth", "Levine" };

        public override string ToString()
        {
            return string.Format( "{0} {1} ({2})", FirstName, LastName, City );
        }

        public static IEnumerable<Person> CreatePeople( int count, string city )
        {
            var people = new List<Person>();

            var r = new Random();

            for ( int i = 0; i < count; i++ )
            {
                var p = new Person()
                {
                    FirstName = firstNames[r.Next( firstNames.Length )],
                    LastName = lastNames[r.Next( lastNames.Length )],
                    City = city
                };
                people.Add( p );
            }
            return people;
        }
    }
}
