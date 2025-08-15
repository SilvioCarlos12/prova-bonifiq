using ProvaPub.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProvaPub.Dtos
{
    public class Pagination<T>    
    {
        public List<T> Itens {  get; private set; }
        public int TotalCount { get; private set; }
        [JsonIgnore]
        public int PageSize { get; private set; }

        public bool HasNext {  get; private set; }

        public Pagination(List<T> itens, int totalCount, int page, int pageSize = 10) {

            Itens = itens;
            TotalCount = totalCount;
            PageSize = pageSize;
            HasNext = totalCount > page * pageSize;
        }


    }
}