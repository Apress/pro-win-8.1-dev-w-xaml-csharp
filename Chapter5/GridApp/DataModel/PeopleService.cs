using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridApp.Data;

namespace GridApp.DataModel
{
    public class PeopleService
    {
        public string[] GetGroups()
        {
            string[] cities = { "Boston", "New York", "LA", "San Francisco", "Phoenix", "San Jose", "Cincinnati", "Bellevue" };
            return cities;
        }

        internal Data.SampleDataGroup GetItems(string group)
        {
            var r = new Random();
            int i = 0;
            var people = Person.CreatePeople(r.Next(50), group);

            SampleDataGroup dataGroup = new SampleDataGroup("Group-" + group,
                        group,
                        string.Empty,
                        "Assets/DarkGray.png",
                        "Group Description: Some description for " + group + " goes here.");

            foreach (var person in people)
            {
                dataGroup.Items.Add(new SampleDataItem("Group-" + group + "-Item-" + ++i,
                    person.FirstName + " " + person.LastName,
                    "(" + person.City + ")",
                    "Assets/LightGray.png",
                    "Person Description: (none)",
                    "Here's where the extended content for each person goes"));
            }
            return dataGroup;
        }
    }
}
