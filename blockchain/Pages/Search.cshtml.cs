using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace blockchain.Pages
{
    public class SearchModel : PageModel
    {
        public void OnGet(string args  )
        {
            var query = Request.QueryString.Value;
            var queryDictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(query);
            var search = queryDictionary["Search"];
            search = Regex.Replace(search, @"\s+", "");
            var deciphered = Model.Recherche.recherche(search);

            Response.Redirect(deciphered);

        }
    }
}