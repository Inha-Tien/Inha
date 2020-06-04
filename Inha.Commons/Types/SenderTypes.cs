namespace Inha.Commons.Types
{
    public enum SenderTypes
    {
        NONE = 0,
        OPSCORE,
        OPSWEB,
        HANDHELD,
        OPS_SERVICE,
        TRAILER_SERVICE,
        HANDHELD_SERVICE,
        TRAILER_REPORT
    }
    public enum ObjectTransferTypes
    {
        NONE = 0,
        WORKINSTRUCTION_HANDHELD,
        WORKINSTRUCTION_TRAILER,
        REPORT_TRAILER
    }
    public enum WorkQueueTypes
    {
        NONE = 0,
        YARDCONSOL,
        GATE
    }

    public enum VesselWIType
    {
        NONE = 0,
        SHPLOAD,
        SHPDISCH
    }
    /// <summary>
    /// EventStoreNames
    /// <para> NONE = 0</para>
    /// <para> WORKINSTRUCTION_HANDHELD_PICKUP</para>
    /// <para> WORKINSTRUCTION_HANDHELD_PLACE</para>
    /// <para> WORKINSTRUCTION_HANDHELD_CREATED</para>
    /// </summary>
    public enum EventStoreNames
    {
        NONE = 0,
        WORKINSTRUCTION_HANDHELD_PICKUP,
        WORKINSTRUCTION_HANDHELD_PLACE,
        WORKINSTRUCTION_HANDHELD_CREATED,
        WORKINSTRUCTION_TRAILER_PENDING,
        WORKINSTRUCTION_TRAILER_DONE,
        WORKINSTRUCTION_TRAILER_INPROGRESS,
        TRAILER_REPORT_UPDATE_RECEIVE_NUMBER,
        TRAILER_REPORT_UPDATE_INPROGRESS_NUMBER,
        TRAILER_REPORT_UPDATE_FINISH_NUMBER
    }
}
