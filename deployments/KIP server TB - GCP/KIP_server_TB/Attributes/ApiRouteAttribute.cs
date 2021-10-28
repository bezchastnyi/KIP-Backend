using System;
using Microsoft.AspNetCore.Mvc;

namespace KIP_Backend.Attributes
{
    /// <summary>
    /// Present attribute for api routing.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RouteAttribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiRouteAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRouteAttribute"/> class.
        /// </summary>
        /// <returns>ApiRouteAttribute.</returns>
        public ApiRouteAttribute()
            : base("v{version:apiVersion}")
        {
        }
    }
}
