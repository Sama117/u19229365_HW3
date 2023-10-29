using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Linq;

namespace HW3.Models
{
    [DataContract]
    public class Points
    {
        public Points(double y, string label)
        {
            this.Y = y;
            this.Label = label;
        }

        [DataMember(Name = "label")]
        public string Label = null;

        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}