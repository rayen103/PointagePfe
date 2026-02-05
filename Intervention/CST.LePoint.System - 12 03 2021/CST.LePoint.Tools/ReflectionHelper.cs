using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace CST.LePoint.Tools
{
    public static class ReflectionHelper
    {
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
            where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false)[0];

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }

            return (T)attribute;
        }

        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property Reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (attr == null)
            {
                return memberInfo.Name;
            }

            return attr.DisplayName;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property Reference expression was found.",
                    "propertyExpression");
            }

            return memberInfo.Name;
        }

        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            var memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                var unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        /// <summary>
        /// Configuration des propriétés d'une classe pour qu'ils soient consultable au niveau GridControl
        /// </summary>
        public static void ConfigurerProprieteClasse(string[] TabAttribute, Object _Object)
        {
            PropertyDescriptorCollection propertyDescriptorlist = TypeDescriptor.GetProperties(_Object.GetType());
            foreach (PropertyDescriptor pd in propertyDescriptorlist)
            {
                BrowsableAttribute browsableAttribute = (BrowsableAttribute)pd.Attributes[typeof(BrowsableAttribute)];
                FieldInfo BrowsableFieldInfo = browsableAttribute.GetType().GetField("Browsable", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                BrowsableFieldInfo.SetValue(browsableAttribute, false);
            }

            for (int i = 0; i < TabAttribute.Length; i++)
            {
                PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(_Object.GetType())[TabAttribute[i]];
                BrowsableAttribute browsableAttribute = (BrowsableAttribute)propertyDescriptor.Attributes[typeof(BrowsableAttribute)];
                FieldInfo BrowsableFieldInfo = browsableAttribute.GetType().GetField("Browsable", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                BrowsableFieldInfo.SetValue(browsableAttribute, true);
            }
        }
    }
}