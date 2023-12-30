using System.Linq;

namespace WebApiUtils
{
    public class DarkConverter
    {
        public static TOut? Convert<TIn, TOut>(TIn? input)
            where TOut : class, new()
        {
            if (input is null) return null;

            var result = new TOut();
            var inProperties = typeof(TIn).GetProperties();
            var outProperties = typeof(TOut).GetProperties();

            foreach (var property in inProperties)
            {
                var outProperty = outProperties.FirstOrDefault(prop => prop.Name == property.Name && prop.PropertyType == property.PropertyType);
                if (outProperty is not null)
                {
                    var value = property.GetValue(input);
                    outProperty.SetValue(result, value);
                }
            }

            return result;
        }
        public static void CopyInto<T>(T? input, T? output)
        {
            if (input is null || output is null) return;

            var inProperties = typeof(T).GetProperties();
            foreach (var property in inProperties)
            {
                var value = property.GetValue(input);
                property.SetValue(output, value);
            }
        }
    }
}
