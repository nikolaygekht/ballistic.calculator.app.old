using Gehtsoft.BallisticCalculator.Connectivity;
using System;

namespace Gehtsoft.BallisticCalculator.DataAccessors
{
    // Read traces from various distanation points 
    // (e.g. a sd card, a phome memory, an internet/intranet network e.t.c.) 
    interface ITraceDataAccessor
    {
        TraceInfoCollection Read();
        bool Write(TraceInfoCollection traceInfoCollection);
    }
}
