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
    public class GrainPlayer : Grain, IGrainPlayer
    {
        //---------------------------------------------------------------------
        ILogger Logger { get; set; }

        //---------------------------------------------------------------------
        public GrainPlayer(ILogger<IGrainPlayer> logger)
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
        Task IGrainPlayer.Test()
        {
            Logger.LogInformation("GrainPlayer.Test()");

            return Task.CompletedTask;
        }
    }
}