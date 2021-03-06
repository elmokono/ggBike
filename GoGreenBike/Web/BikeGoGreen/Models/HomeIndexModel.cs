﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeGoGreen.Models
{
    public class StatisticsRunner
    {
        public Int32 Id { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
    }

    public class RunnerModel
    {
        public Model.Runner Runner { get; set; }
        public IEnumerable<Model.Group> Groups { get; set; }
    }

    public class HomeIndexModel
    {

    }
}