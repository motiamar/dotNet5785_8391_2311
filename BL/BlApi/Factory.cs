﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi;

/// <summary>
/// the BL factory
/// </summary>
public static class Factory
{
    public static IBl Get() => new BlImplementation.BI();
}
