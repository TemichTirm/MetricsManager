using Dapper;
using System;
using System.Data;

namespace MetricsAgent.DTO
{

    //public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    //{
    //    private readonly DateTimeOffset baseTime = new(new(2000, 01, 01));
    //    public override DateTimeOffset Parse(object value) => baseTime.AddSeconds((double)value);
    //    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
    //                                    => parameter.Value = (value - baseTime).TotalSeconds;        
    //}
}
