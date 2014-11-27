using System;
using System.Runtime.Serialization;

namespace CodeUtopia.Messaging.EasyNetQ
{
    public class UnexpectedMessageException : Exception
    {
        public UnexpectedMessageException(Type messageType)
            : base(string.Format("The message '{0}' was not expected.", messageType))
        {
            _messageType = messageType;
        }

        protected UnexpectedMessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _messageType = (Type)info.GetValue("MessageType", typeof(Type));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("MessageType", MessageType);
        }

        public Type MessageType
        {
            get
            {
                return _messageType;
            }
        }

        private readonly Type _messageType;
    }
}