using Mvc5Application1.Data.Model;
using System;

namespace Mvc5Application1.Infrastructure.Unity
{
    public class Unity
    {
        public static MessageEnum GetMessageEnum(string messageVal)
        {
            var message = MessageEnum.Empty;
            if (!string.IsNullOrEmpty(messageVal))
            {
                message = (MessageEnum)Enum.Parse(typeof(MessageEnum), messageVal);
            }

            return message;
        }
    }
}