using System.Text;

namespace LangExperiments
{
    static class Extensions
        {
public static string ToProperties(this object o)
            {
                var sb = new StringBuilder();
            var type = o.GetType();
                foreach (var prop in o.GetType().GetProperties())
                {
                    foreach (var att in prop.GetCustomAttributes(false))
                    {
                        if (att is ToStringAttribute)
                        {
                            sb.Append($"{prop.Name}: {prop.GetValue(o)}");
                            break;
                        }
                    }
                }
                
                return sb.ToString();
            }
        }
}