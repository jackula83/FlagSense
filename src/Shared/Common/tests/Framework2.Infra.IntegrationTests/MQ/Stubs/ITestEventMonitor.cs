﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework2.Infra.IntegrationTests.MQ.Stubs
{
    public interface ITestEventMonitor
    {
        void EventMonitored(string correlationId);
    }
}
