// Copyright (c) Cragon. All rights reserved.

namespace Cragon
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Runtime;

    public class Startup : IStartupTask
    {
        //---------------------------------------------------------------------
        ILogger Logger { get; set; }
        IGrainFactory GrainFactory { get; set; }

        //---------------------------------------------------------------------
        public Startup(IGrainFactory grain_factory, ILogger<IStartupTask> logger)
        {
            GrainFactory = grain_factory;
            Logger = logger;
        }

        //---------------------------------------------------------------------
        public async Task Execute(CancellationToken cancellationToken)
        {
            var grain_account = GrainFactory.GetGrain<IGrainAccount>(Guid.NewGuid());
            await grain_account.Test();
        }
    }
}