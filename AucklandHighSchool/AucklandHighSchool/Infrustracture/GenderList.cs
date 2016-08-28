using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AucklandHighSchool.Infrustracture
{
    public static class GenderList
    { 
        public static List<SelectListItem> CreateGenderList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "M",
                    Text = "Male",
                },

                new SelectListItem
                {
                    Value = "F",
                    Text = "Female"
                }
            };
        }
    }
}