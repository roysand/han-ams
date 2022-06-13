// ============================================================================
//    Author: Kenneth Perkins
//    Date:   May 10, 2021
//    Taken From: http://programmingnotes.org/
//    File:  Utils.cs
//    Description: Handles general utility functions
// ============================================================================
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Helpers
{


    public static class HttpParams {
        /// <summary>
        /// Adds the query string parameters to a url
        /// </summary>
        /// <param name="url">The url to append parameters</param> 
        /// <param name="query">The parameters to add to the url</param> 
        /// <returns>The url appended with the specified query</returns>
        public static string Add(string url, System.Collections.Specialized.NameValueCollection query) {
            return UpdateQuery(url, queryString => {
                var merged = new System.Collections.Specialized.NameValueCollection {
                    queryString,
                    query
                };
                CopyProps(merged, queryString);
            });
        }

        /// <summary>
        /// Adds the query string parameters to a url
        /// </summary>
        /// <param name="url">The url to append parameters</param> 
        /// <param name="param">The parameters to add to the url</param> 
        /// <returns>The url appended with the specified query</returns>
        public static string Add(string url, KeyValuePair<string, string> param)  {
            return Add(url, new System.Collections.Specialized.NameValueCollection() { 
                { param.Key, param.Value } 
            });
        }

        /// <summary>
        /// Updates the query string parameters to a url
        /// </summary>
        /// <param name="url">The url to update parameters</param> 
        /// <param name="query">The parameters to update to the url</param> 
        /// <returns>The url updated with the specified query</returns>
        public static string Update(string url, System.Collections.Specialized.NameValueCollection query) {
            return UpdateQuery(url, queryString => {
                CopyProps(query, queryString);
            });
        }

        /// <summary>
        /// Updates the query string parameters to a url
        /// </summary>
        /// <param name="url">The url to update parameters</param> 
        /// <param name="param">The parameters to update to the url</param> 
        /// <returns>The url updated with the specified query</returns>
        public static string Update(string url, KeyValuePair<string, string> param)  {
            return Update(url, new System.Collections.Specialized.NameValueCollection() { 
                { param.Key, param.Value } 
            });
        }

        /// <summary>
        /// Removes the parameters with the matching keys from the query string of a url
        /// </summary>
        /// <param name="url">The url to remove parameters</param> 
        /// <param name="keys">The parameter keys to remove from the url</param> 
        /// <returns>The url with the parameters removed</returns>
        public static string Remove(string url, IEnumerable<string> keys) {
            return UpdateQuery(url, queryString => {
                foreach (var key in keys) {
                    if (queryString.AllKeys.Contains(key)) { 
                        queryString.Remove(key);
                    }
                }
            });
        }

        /// <summary>
        /// Removes the parameter with the matching key from the query string of a url
        /// </summary>
        /// <param name="url">The url to remove a parameter</param> 
        /// <param name="key">The parameter key to remove from the url</param> 
        /// <returns>The url with the parameter removed</returns>
        public static string Remove(string url, string key) {
            return Remove(url, new string[]{ key });
        }

        /// <summary>
        /// Removes all parameters from the query string of a url
        /// </summary>
        /// <param name="url">The url to clear parameters</param>  
        /// <returns>The url with the parameters removed</returns>
        public static string Clear(string url) {
            return UpdateQuery(url, queryString => {
                queryString.Clear();
            });
        }

        /// <summary>
        /// Returns all parameters from the query string of a url
        /// </summary>
        /// <param name="url">The url to get parameters</param>  
        /// <returns>The url parameters</returns>
        public static System.Collections.Specialized.NameValueCollection Get(string url) {
            System.Collections.Specialized.NameValueCollection result = null;
            UpdateQuery(url, queryString => {
                result = queryString;
            });
            return result;
        }

        /// <summary>
        /// Sets the query string parameters to a url
        /// </summary>
        /// <param name="url">The url to set parameters</param> 
        /// <param name="query">The parameters to set to the url</param> 
        /// <returns>The url set with the specified query</returns>
        public static string Set(string url, System.Collections.Specialized.NameValueCollection query) {
            url = Clear(url);
            return Add(url, query);
        }

        /// <summary>
        /// Sets the query string parameters to a url
        /// </summary>
        /// <param name="url">The url to set parameters</param> 
        /// <param name="param">The parameters to set to the url</param> 
        /// <returns>The url set with the specified query</returns>
        public static string Set(string url, KeyValuePair<string, string> param)  {
            return Set(url, new System.Collections.Specialized.NameValueCollection() { 
                { param.Key, param.Value } 
            });
        }

        private static void CopyProps(System.Collections.Specialized.NameValueCollection source, System.Collections.Specialized.NameValueCollection destination) { 
            foreach (string key in source) {
                destination[key] = source[key];
            }        
        }

        private static string UpdateQuery(string url, Action<System.Collections.Specialized.NameValueCollection> modifyQuery) {
            var uriBuilder = new System.UriBuilder(url);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            modifyQuery(query);
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri.ToString();
        }
    }
}