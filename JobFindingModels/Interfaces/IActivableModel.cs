﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels.Interfaces
{
    public interface IActivableModel
    {
        public bool IsActive { get; set; }
    }
}