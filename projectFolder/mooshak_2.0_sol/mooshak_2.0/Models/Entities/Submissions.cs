﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mooshak_2._0.Models.Entities
{
    public class Submissions : IEnumerable
    {
        public int Id { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public string UserName { get; set; }
        //public int AssignmentId { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}