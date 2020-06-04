using System.Collections.Generic;

namespace Inha.Commons.Messages
{
    public class TrailerNotify
    {
        public string Status { get; set; }

    }
    public interface IWITrailer
    {
        string Status { get; }
    }
    public class WITrailerTransfer
    {
        public PendingWITrailer WIPending { get; set; }
        public IEnumerable<InprogressWITrailer> WIInProgress { get; set; }
        public DoneWITrailer WIDone { get; set; }
        public string Status { get; set; }

    }
    public class PendingWITrailer : IWITrailer
    {

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get { return "PENDING"; } }
        /// <summary>
        /// SiteId
        /// </summary>
        public string SiteGuid { get; set; }

        /// <summary>
        /// BatNo
        /// </summary>
        public string BatNo { get; set; }

        /// <summary>
        /// WorkIntructionTrailerId
        /// </summary>
        public string WorkIntructionTrailerId { get; set; }
        /// <summary>
        /// WIIdRef
        /// </summary>
        public string WIIdRef { get; set; }

        /// <summary>
        /// Số container
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// FromYardLoc
        /// </summary>
        public string FromYardLoc { get; set; }

        /// <summary>
        /// ToYardLoc
        /// </summary>
        public string ToYardLoc { get; set; }


        /// <summary>
        /// UserId login
        /// </summary>
        public string UserGuid { get; set; }
    }
    public class InprogressWITrailer : IWITrailer
    {
        public string WorkInstructionGuid { get; set; }
        public string PlanLoc { get; set; }
        public string UserGuid { get; set; }
        public string SiteGuid { get; set; }
        public string Status { get { return "INPROGRESS"; } }
    }
    public class DoneWITrailer : IWITrailer
    {
        /// <summary>
        /// WorkInstructionTrailerGuid
        /// </summary>
        public string WorkInstructionGuid { get; set; }
        /// <summary>
        /// BatNo
        /// </summary>
        //public string BatNo { get; }
        /// <summary>
        /// SiteId
        /// </summary>
        public string SiteGuid { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public string UserGuid { get; set; }
        public string Status { get { return "DONE"; } }
    }
}
