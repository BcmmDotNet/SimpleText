#if !NET5_0 && !NET6_0  

namespace System.Runtime.CompilerServices
{
    // 为什么定义这个类型，参见：https://developercommunity.visualstudio.com/t/error-cs0518-predefined-type-systemruntimecompiler/1244809
    internal static class IsExternalInit { }
}

#endif