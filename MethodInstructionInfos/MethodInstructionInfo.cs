using Mono.Reflection;
using System.Collections.Generic;
using System.Reflection;

namespace MethodInstructionInfos
{
	public static class MethodInstructionInfo
	{
		/// <summary>
		/// Gets the fields handled in the method.
		/// </summary>
		public static HashSet<FieldInfo> GetHandledFields(this MethodBase self)
		{
			var buffer = new HashSet<FieldInfo>();
			foreach (var instruction in self.GetInstructions())
			{
				var fieldInfo = instruction.Operand as FieldInfo;
				if(fieldInfo != null)
					buffer.Add(fieldInfo);
			}
			return buffer;
		}

		/// <summary>
		/// Gets the methods handled in the method.
		/// </summary>
		public static HashSet<MethodInfo> GetHandledMethods(this MethodBase self)
		{
			var buffer = new HashSet<MethodInfo>();
			foreach(var instruction in self.GetInstructions())
			{
				var fieldInfo = instruction.Operand as MethodInfo;
				if(fieldInfo != null)
					buffer.Add(fieldInfo);
			}
			return buffer;
		}

		/// <summary>
		/// Gets the fields handled in the method and the methods it is calling.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="ignoreTransparent">Ignore methods that are transparent at the current trust level and therefore their handled fields cannot be extracted.</param>
		/// <param name="maxDepth">The maximum depth level of the recursion</param>
		public static HashSet<FieldInfo> GetRecursiveHandledFields(this MethodBase self, bool ignoreTransparent = true,
			uint maxDepth = uint.MaxValue) => getRecursiveHandledFields(self, 0, ignoreTransparent, maxDepth);
		static HashSet<FieldInfo> getRecursiveHandledFields(MethodBase self, uint depth, bool ignoreTransparent,
			uint maxDepth)
		{
			depth++;
			if(ignoreTransparent && self.IsSecurityTransparent)
				return new HashSet<FieldInfo>();

			HashSet<FieldInfo> fields = self.GetHandledFields();

			if(depth <= maxDepth)
				foreach (var method in self.GetHandledMethods())
					fields.UnionWith(getRecursiveHandledFields(method, depth, ignoreTransparent, maxDepth));

			return fields;
		}
	}
}
