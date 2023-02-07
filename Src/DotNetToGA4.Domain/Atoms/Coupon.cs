using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToGA4.Domain.Atoms;
public record Coupon(string data) : AtomBase<string>(data);

public record PaymentType(string data) : AtomBase<string>(data);
public record EventName(string data) : AtomBase<string>(data);
public record Currency(string data) : AtomBase<string>(data)
{
    public static Currency Nok { get; internal set; }
}

public record GaValue(decimal data) : AtomBase<decimal>(data);
public record ItemId(string data) : AtomBase<string>(data); 
public record ItemName(string data) : AtomBase<string>(data);   

public record class AtomBase<T>(T data) where T : IConvertible
{
    public T Value { get { return data; } }

    public static implicit operator AtomBase<T>(T data)
    {
        // While not technically a requirement; see below why this is done.
        if (data == null)
            return null;

        return new AtomBase<T>(data);
    }
}