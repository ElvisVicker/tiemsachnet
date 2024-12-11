using Newtonsoft.Json;
using System.Text.Json;
using tiemsach.ViewModels;

namespace tiemsach.Helper
{
    public static class SessionExtensions
    {
        //public static void Set<T>(this ISession session, string key, T value)
        //{
        //    session.SetString(key, JsonSerializer.Serialize(value));
        //}



        //public static T? Get<T>(this ISession session, string key)
        //{
        //    var value = session.GetString(key);
        //    return value == null ? default : JsonSerializer.Deserialize<T>(value);
        //}
        public static List<CartItem> GetCart(HttpRequest request)
        {
            var cartJson = request.Cookies["Cart"];
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        }

        public static void SaveCart(HttpResponse response, List<CartItem> cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            response.Cookies.Append("Cart", cartJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });
        }

    }
}
