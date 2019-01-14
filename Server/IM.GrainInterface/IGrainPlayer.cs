// Copyright(c) Cragon. All rights reserved.

namespace Cragon
{
    using System;
    using System.Threading.Tasks;
    using Orleans;

    public interface IGrainPlayer : IGrainWithGuidKey
    {
        //---------------------------------------------------------------------
        Task Test();
    }
}