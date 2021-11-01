using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace sourceTree
{
    public class SubTopic : Topic
    {
        //[ForeignKey(typeof(Topic))]
        //public int parentTopicId { get; set; }
    }
}
