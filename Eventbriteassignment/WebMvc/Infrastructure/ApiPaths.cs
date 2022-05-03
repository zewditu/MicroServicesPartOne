namespace WebMvc.Infrastructure
{
    public static class ApiPaths
    {
        public static class Events
        {
            public static string GetAllCategories(string baseUri)
            {
                return $"{baseUri}/eventcategories";
            }
            public static string GetAllPlaces(string baseUri)
            {
                return $"{baseUri}/eventplaces";
            }
            public static string GetAllEvents(string baseUri, int pageNumber, int pageSize, int? categoryId, int? placeId)
            {
                var preUri = string.Empty;
                var filterQs = string.Empty;
                if (categoryId.HasValue)
                {
                    filterQs = $"catalogbrandid={categoryId.Value}";
                }
                if (placeId.HasValue)
                {
                    filterQs = (filterQs == string.Empty) ?
                        $"catalogtypeid={placeId.Value}" :
                        $"&catalogtypeid={placeId.Value}";
                }

                if (string.IsNullOrEmpty(filterQs))
                {
                    preUri = $"{baseUri}/events?pageindex={pageNumber}&pagesize={pageSize}";
                }
                else
                {
                    preUri = $"{baseUri}/events/filter?pageindex={pageNumber}&pagesize={pageSize}&{filterQs}";
                }
                return preUri;
            }
        }
    }
}
