using System;
using System.Net;
using System.Runtime.Serialization;

namespace BookApp.Helper
{

    [Serializable]
    [DataContract]
    public class APIDataException : Exception, IAPIException
    {
        #region Public Serializable properties.

        [DataMember]
        public int ErrorCode { get; set; } = 0;

        [DataMember]
        public string ErrorDescription { get; set; } = "Ressouce was not found.";

        [DataMember]
        public HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.NotFound;

        [DataMember]
        public string ReasonPhrase { get; set; } = "";

        #endregion
    }
}