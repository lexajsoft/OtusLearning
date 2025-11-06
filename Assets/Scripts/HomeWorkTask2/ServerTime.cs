using System;

namespace HomeWorkTask2
{
    public class ServerTime
    {
        public static DateTime GetCurrentTime()
        {
            // для взятия времени с сервера, но так как его тут нет предположим что оно у нас есть
            return DateTime.UtcNow;
        }
    }
}