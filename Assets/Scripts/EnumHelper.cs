using System;
using System.Collections.Generic;
using System.Linq;

public class EnumHelper<T>
{
    public static IEnumerable<T> Iterator => Enum.GetValues(typeof(T)).Cast<T>();
}