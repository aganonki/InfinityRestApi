using System;
using System.Collections.Generic;
using System.Text;

namespace InfinityRest.Shared.Enums
{
    public enum ProcessStateEnum
    {
        None = 0,
        Waiting = 10,
        ReadyToProcess = 20,
        Processing = 30,
        Failed = 40,
        Completed = 50
    }
}
