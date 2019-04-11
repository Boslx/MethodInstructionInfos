# MethodInstructionInfos
The class MethodInstructionInfo.cs extends MethodBase in .NET by the option to read out which fields and methods the method handles. 

## About
The library tries to extract the variables/methods handled in it from its IL code. To get the IL code, it relies on [Mono.Reflection](https://github.com/jbevain/mono.reflection). The functionality of the library is ensured in unit tests.

## Documentation

```csharp
/// <summary>
/// Gets the fields handled in the method.
/// </summary>
public static HashSet<FieldInfo> GetHandledFields(this MethodBase self)
```

```csharp
/// <summary>
/// Gets the fields handled in the method and the methods it is calling.
/// </summary>
/// <param name="self"></param>
/// <param name="ignoreTransparent">Ignore methods that are transparent at the current trust level and therefore their handled fields cannot be extracted.</param>
/// <param name="maxDepth">The maximum depth level of the recursion</param>
public static HashSet<FieldInfo> GetRecursiveHandledFields(this MethodBase self, bool ignoreTransparent = true, uint maxDepth = uint.MaxValue)
```

```csharp
/// <summary>
/// Gets the methods handled in the method.
/// </summary>
public static HashSet<MethodInfo> GetHandledMethods(this MethodBase self)
```
