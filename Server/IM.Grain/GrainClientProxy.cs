// Copyright(c) Cragon. All rights reserved.

namespace Cragon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Concurrency;

    [Reentrant]
    public class GrainClientProxy : Grain, IGrainClientProxy
    {
        //---------------------------------------------------------------------
        ILogger Logger { get; set; }

        //---------------------------------------------------------------------
        public GrainClientProxy(ILogger<IGrainClientProxy> logger)
        {
            Logger = logger;
        }

        //---------------------------------------------------------------------
        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        //---------------------------------------------------------------------
        public override Task OnDeactivateAsync()
        {
            return base.OnDeactivateAsync();
        }

        //---------------------------------------------------------------------
        Task IGrainClientProxy.Test()
        {
            Logger.LogInformation("GrainClientProxy.Test()");

            return Task.CompletedTask;
        }
    }
}