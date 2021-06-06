using System;
using System.Net;
using System.Runtime.Serialization;

namespace BookApp.Helper
{

    [Serializable]
    [DataContract]
    public class APIException : Exception, IAPIException
    {
        #region Public Serializable properties.

        [DataMember]
        public int ErrorCode { get; set; } = (int)HttpStatusCode.BadRequest;

        [DataMember]
        public string ErrorDescription { get; set; } = "Bad Request. Provide valid object. Object can't be null.";

        [DataMember]
        public HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.BadRequest;

        [DataMember]
        public string ReasonPhrase { get; set; } = "ApiException";

        #endregion
    }
}