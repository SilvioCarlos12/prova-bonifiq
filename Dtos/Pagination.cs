namespace ProvaPub.Dtos
{
    public class Pagination<T>    
    {
        public List<T> Itens {  get; private set; }
        public int TotalCount { get; private set; }
        public bool HasNext {  get; private set; }

        public Pagination(List<T> itens, int totalCount, int page) {

            Itens = itens;
            TotalCount = totalCount;
            HasNext = totalCount > page * itens.Count ;
        }


    }
}