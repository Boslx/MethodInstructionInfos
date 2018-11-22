using Mono.Reflection;
using System.Collections.Generic;
using System.Reflection;

namespace MethodInstructionInfos
{
    public static class MethodeInstructionInfo
    {
        /// <summary>
        /// Gets the fields handled in the method.
        /// </summary>
        public static HashSet<FieldInfo> GetHandledFields(this MethodBase self)
        {
            var buffer = new HashSet<FieldInfo>();
            if (!self.IsSecurityTransparent)
                foreach (var instruction in self.GetInstructions())
                {
                    var fieldInfo = instruction.Operand as FieldInfo;
                    if (fieldInfo != null)
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
            if (!self.IsSecurityTransparent)
                foreach (var instruction in self.GetInstructions())
                {
                    var fieldInfo = instruction.Operand as MethodInfo;
                    if (fieldInfo != null)
                        buffer.Add(fieldInfo);
                }
            return buffer;
        }

        /// <summary>
        /// Gets the fields handled in the method and the methodes it is calling.
        /// </summary>
        /// <param name="maxDepth">The maximum depth level of the recursion</param>
        /// <returns></returns>
        public static HashSet<FieldInfo> GetRecursiveHandledFields(this MethodBase self, uint maxDepth = uint.MaxValue) => getRecursiveHandeldFields(self, 0, maxDepth);
        static HashSet<FieldInfo> getRecursiveHandeldFields(MethodBase self, uint depth, uint maxDepth)
        {
            depth++;
            HashSet<FieldInfo> fields = self.GetHandledFields();

            if (depth <= maxDepth)
                foreach (var methode in self.GetHandledMethods())
                    fields.UnionWith(getRecursiveHandeldFields(methode, depth, maxDepth));

            return fields;
        }
    }
}
