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
    public class GrainAccount : Grain, IGrainAccount
    {
        //---------------------------------------------------------------------
        ILogger Logger { get; set; }

        //---------------------------------------------------------------------
        public GrainAccount(ILogger<IGrainAccount> logger)
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
        Task IGrainAccount.Test()
        {
            Logger.LogInformation("GrainAccount.Test()");

            return Task.CompletedTask;
        }
    }
}