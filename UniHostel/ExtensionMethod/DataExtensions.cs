using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UniHostel.ExtensionMethod
{
    public static class DataExtensions
    {
        public static void Remove<TEntity>(
            this System.Data.Entity.DbSet<TEntity> entities,
            System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var records = entities
                .Where(predicate)
                .ToList();
            if (records.Count > 0)
                entities.RemoveRange(records);
        }

        public static string ComputeSha256Hash(this string rawData)
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }  
        }

        public static string RenderPartial(this ControllerContext context, string partialName, object model)
        {
            var sw = new StringWriter();

            var httpContext = new HttpContextWrapper(HttpContext.Current);

            var view = ViewEngines.Engines.FindPartialView(context, partialName).View;

            view.Render(new ViewContext(context, view, new ViewDataDictionary { Model = model }, context.Controller.TempData, sw), sw);

            return sw.ToString();
        }
    }
}