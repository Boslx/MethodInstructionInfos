# MethodInstructionInfos
The class MethodInstructionInfo.cs extends MethodBase in .NET by the option to read out which fields and methods the method handles. 

## About
The library tries to extract the variables/methods handled in it from its IL code. To get the IL code, it relies on [Mono.Reflection](https://github.com/jbevain/mono.reflection). The functionality of the library is ensured in unit tests.

## Documentation

```csharp
// Gets the fields handled in the method.
public static HashSet<FieldInfo> GetHandledFields(this MethodBase self)
```

```csharp
// Gets the fields handled in the method and the methodes it is calling.
// Sets the maximum depth level of the recursion
public static HashSet<FieldInfo> GetRecursiveHandledFields(this MethodBase self, uint maxDepth = uint.MaxValue)
```

```csharp
// Gets the methods handled in the method.
public static HashSet<MethodInfo> GetHandledMethods(this MethodBase self)
```
