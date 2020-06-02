using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Web_Service.Models
{
    //This class was created to test xml serialization
    public class Query
    {
		[Key]
		public int id_query { get; set; }
		[XmlElement(ElementName = "Group")]
		public List<Group> Group { get; set; }

	}

	[XmlRoot(ElementName = "Group")]
	public class Group  
	{
		[Key]
		public int id_group { get; set; }

		[XmlElement(ElementName = "AndOr")]
		public string AndOr { get; set; }

		[XmlElement(ElementName = "Field")]	
		public string Field { get; set; }
		[XmlElement(ElementName = "Operator")]
		public string Operator { get; set; }
		[XmlElement(ElementName = "Value")]
		public string Value { get; set; }
		

	}
}
